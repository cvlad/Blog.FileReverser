namespace FileReverser.App
{
    public class InputFileValidator
    {
        private readonly IFileChecker _fileChecker;
        private readonly ITextFileChecker _textFileChecker;
        private readonly IPermissionChecker _permissionsChecker;
        private readonly string _fileName;

        public InputFileValidator(IFileChecker fileChecker, ITextFileChecker textFileChecker, IPermissionChecker permissionChecker, string fileName)
        {
            _fileChecker = fileChecker;
            _textFileChecker = textFileChecker;
            _permissionsChecker = permissionChecker;
            _fileName = fileName;
        }

        public int Validate()
        {
            if (!_fileChecker.FileExists(_fileName))
                return 1;
            if (!_textFileChecker.IsTextFile(_fileName))
                return 2;
            if (!_permissionsChecker.CanReadFile(_fileName))
                return 3;
            return 0;
        }
    }
}