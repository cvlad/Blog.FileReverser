using System;
using System.Security.Permissions;

namespace FileReverser.App
{
    public class FileCreate : IFileCreate
    {
        public bool CanCreate(string fileName)
        {
            try
            {
                new FileIOPermission(FileIOPermissionAccess.Write, fileName).Demand();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}