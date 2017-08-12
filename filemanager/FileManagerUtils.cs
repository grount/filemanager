﻿using System;
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

            if (logicalDrives.All(i_CurrentPath.Contains))
            {
                i_FileType = eFileType.LogicalDrive;
                state = true;
            }

            return state;
        }
    }
}
