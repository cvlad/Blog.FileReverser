namespace FileReverser.App
{
    public class OutputFileValidator : IOutputValidator
    {
        private readonly IFileChecker _fileChecker;
        private readonly IFileCreate _fileCreate;

        public OutputFileValidator(IFileChecker fileChecker, IFileCreate fileCreate)
        {
            _fileChecker = fileChecker;
            _fileCreate = fileCreate;
        }

        public int Validate(string output)
        {
            if(_fileChecker.FileExists(output))
                return 4;
            if (!_fileCreate.CanCreate(output))
                return 5;
            return 0;
        }
    }
}