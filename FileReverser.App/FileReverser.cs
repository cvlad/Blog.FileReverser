namespace FileReverser.App
{
    public class FileReverser : IFileReverser
    {
        private readonly IOutputter _outputter;
        private readonly IInput _input;
        private readonly IInputValidator _validator;

        public const string InputMessage = "Please enter the full path and name of the input file: ";

        public FileReverser(IOutputter outputter, IInput input, IInputValidator validator)
        {
            _outputter = outputter;
            _input = input;
            _validator = validator;
        }

        public void PromptForInput()
        {
            _outputter.Write(InputMessage);
        }

        public string ReadInput()
        {
            return _input.Read();
        }

        public int? ValidateInput(string file)
        {
            var result = _validator.Validate(file);
            return result == 0 ? null : (int?)result;
        }
    }
}