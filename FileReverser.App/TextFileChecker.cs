namespace FileReverser.App
{
    class TextFileChecker : ITextFileChecker
    {
        public bool IsTextFile(string fileName)
        {
            return new System.IO.FileInfo(fileName).Extension == ".txt";
        }
    }
}