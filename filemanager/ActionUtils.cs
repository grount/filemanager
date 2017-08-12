using System;
using System.IO;
using System.Windows.Forms;

namespace filemanager
{
    public static class ActionUtils
    {
        public static void Perform(eActionType i_ActionType, string i_OriginPath, string io_DestinationPath)
        {
            if (io_DestinationPath == Path.GetDirectoryName(i_OriginPath) && i_ActionType == eActionType.Move)
                return; 

            ProcessDestinationPath(i_OriginPath, ref io_DestinationPath);

            switch (FileManagerUtils.GetCurrentPathType(i_OriginPath))
            {
                case eFileType.File:
                    handleFileType(i_ActionType, i_OriginPath, io_DestinationPath);
                    break;
                case eFileType.Folder:
                    handleFolderType(i_ActionType, i_OriginPath, io_DestinationPath);
                    break;
                case eFileType.Invalid:
                    break;
                case eFileType.LogicalDrive:
                    MessageBox.Show("You cannot move logical drive.", "Windows Explorer",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static void handleFolderType(eActionType i_ActionType, string i_OriginPath, string i_DestinationPath)
        {
            if (i_ActionType == eActionType.Move)
            {
                Directory.Move(i_OriginPath, i_DestinationPath); // todo handle moving files with the same name?
            }
            else
            {
                CopyAll(new DirectoryInfo(i_OriginPath), new DirectoryInfo(i_DestinationPath));
            }
        }

        private static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            // Copy each file into the new directory.
            foreach (FileInfo file in source.GetFiles())
            {
                file.CopyTo(Path.Combine(target.FullName, file.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }

        private static void handleFileType(eActionType i_ActionType, string i_OriginPath, string i_DestinationPath)
        {
            if (i_ActionType == eActionType.Move)
            {
                File.Move(i_OriginPath, i_DestinationPath);
            }
            else
            {
                File.Copy(i_OriginPath, i_DestinationPath);
            }
        }

        private static void ProcessDestinationPath(string i_OriginPath, ref string io_DestinationPath)
        {
            if (FileManagerUtils.GetCurrentPathType(io_DestinationPath) != eFileType.LogicalDrive)
            {
                io_DestinationPath += "\\" + Path.GetFileName(i_OriginPath);
            }
            else
            {
                io_DestinationPath += Path.GetFileName(i_OriginPath);
            }

            if (i_OriginPath.Equals(io_DestinationPath))
            {
                while (File.Exists(io_DestinationPath) || Directory.Exists(io_DestinationPath))
                {
                    io_DestinationPath = Path.GetDirectoryName(io_DestinationPath) + Path.GetFileNameWithoutExtension(io_DestinationPath) + " - Copy" +
                                        Path.GetExtension(i_OriginPath);
                }
            }
        }
    }
}