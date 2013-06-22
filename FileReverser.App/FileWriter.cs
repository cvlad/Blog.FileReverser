namespace FileReverser.App
{
    class FileWriter : IFileWriter
    {
        public void Write(string file, string content)
        {
            System.IO.File.WriteAllText(file, content);
        }
    }
}