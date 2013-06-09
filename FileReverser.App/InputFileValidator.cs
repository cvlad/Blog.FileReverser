namespace FileReverser.App
{
    public class InputFileValidator : IInputValidator
    {
        private readonly IFileChecker _fileChecker;
        private readonly ITextFileChecker _textFileChecker;
        private readonly IPermissionChecker _permissionsChecker;

        public InputFileValidator(IFileChecker fileChecker, ITextFileChecker textFileChecker, IPermissionChecker permissionChecker)
        {
            _fileChecker = fileChecker;
            _textFileChecker = textFileChecker;
            _permissionsChecker = permissionChecker;
        }

        public int Validate(string fileName)
        {
            if (!_fileChecker.FileExists(fileName))
                return 1;
            if (!_textFileChecker.IsTextFile(fileName))
                return 2;
            if (!_permissionsChecker.CanReadFile(fileName))
                return 3;
            return 0;
        }
    }
}