using System.Linq;

namespace FileReverser.App
{
    public class ReverserImplementation : IReverserImplementation
    {
        private readonly IFileReader _fileReader;
        private readonly IFileWriter _fileWriter;

        public ReverserImplementation(IFileReader fileReader, IFileWriter fileWriter)
        {
            _fileReader = fileReader;
            _fileWriter = fileWriter;
        }

        public void Reverse(string inputFile, string outputFile)
        {
            var content = _fileReader.Read(inputFile);
            var reversedContent = new string(content.Reverse().ToArray());
            _fileWriter.Write(outputFile, reversedContent);
        }
    }
}