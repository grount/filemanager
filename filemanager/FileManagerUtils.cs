using System;
using System.IO;
using System.Linq;

namespace filemanager
{
    public static class FileManagerUtils
    {
        public static eFileType GetCurrentPathType(string i_CurrentPath)
        {
            eFileType fileType = eFileType.Invalid;

            if (checkIfFileTypeIsLogicalDrive(i_CurrentPath, ref fileType)) return fileType;

            try
            {
                FileAttributes attributes = File.GetAttributes(i_CurrentPath);

                fileType = attributes.HasFlag(FileAttributes.Directory) ? eFileType.Folder : eFileType.File;
            }
            catch (Exception e)
            {
                fileType = eFileType.Invalid;
            }

            return fileType;
        }

        private static bool checkIfFileTypeIsLogicalDrive(string i_CurrentPath, ref eFileType i_FileType)
        {
            string[] logicalDrives = Environment.GetLogicalDrives();
            bool state = false;

            foreach (var drive in logicalDrives)
            {
                if (drive.Equals(i_CurrentPath))
                {
                    i_FileType = eFileType.LogicalDrive;
                    state = true;
                }
            }

            return state;
        }

        public static void messagePreperations(ref string i_Message, eActionType i_ActionType)
        {
            switch (i_ActionType)
            {
                case eActionType.Copy:
                    i_Message = "copy";
                    break;
                case eActionType.Move:
                    i_Message = "move";
                    break;
                case eActionType.Delete:
                    i_Message = "delete";
                    break;
            }
        }
    }
}
