namespace FileReverser.App
{
    public interface IPermissionChecker
    {
        bool CanReadFile(string fileName);
    }
}