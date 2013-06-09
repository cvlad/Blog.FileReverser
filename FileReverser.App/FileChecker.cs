namespace FileReverser.App
{
    class FileChecker : IFileChecker
    {
        public bool FileExists(string fileName)
        {
            return System.IO.File.Exists(fileName);
        }
    }
}