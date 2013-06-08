namespace FileReverser.App
{
    public interface IFileChecker
    {
        bool FileExists(string fileName);
    }
}