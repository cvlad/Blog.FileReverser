using System;
using System.Security.Permissions;

namespace FileReverser.App
{
    public class PermissionChecker : IPermissionChecker
    {
        public bool CanReadFile(string fileName)
        {
            try
            {
                new FileIOPermission(FileIOPermissionAccess.Read, fileName).Demand();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}