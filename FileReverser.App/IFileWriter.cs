namespace FileReverser.App
{
    public interface IFileWriter
    {
        void Write(string file, string content);
    }
}