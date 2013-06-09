namespace FileReverser.App
{
    public class TextFileChecker : ITextFileChecker
    {
        public bool IsTextFile(string fileName)
        {
            return new System.IO.FileInfo(fileName).Extension == ".txt";
        }
    }
}