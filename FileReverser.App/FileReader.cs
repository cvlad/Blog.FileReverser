namespace FileReverser.App
{
    class FileReader : IFileReader
    {
        public string Read(string file)
        {
            return System.IO.File.ReadAllText(file);
        }
    }
}