using System;
using System.IO;
using System.Windows.Forms;

namespace filemanager
{
    public static class MoveUtils
    {
        public static void Move(string i_OriginPath, string i_DestinationPath)
        {
            if (i_DestinationPath != i_OriginPath)
            {
                switch (FileManagerUtils.GetCurrentPathType(i_OriginPath))
                {
                    case eFileType.File:
                        ProcessDestinationPath(i_OriginPath, ref i_DestinationPath);
                        File.Move(i_OriginPath, i_DestinationPath);
                        break;
                    case eFileType.Folder:
                        ProcessDestinationPath(i_OriginPath, ref i_DestinationPath);
                        Directory.Move(i_OriginPath, i_DestinationPath);
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
        }

        private static void ProcessDestinationPath(string i_OriginPath, ref string o_DestinationPath)
        {
            o_DestinationPath += "\\" + Path.GetFileName(i_OriginPath);
        }
    }
}