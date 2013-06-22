namespace FileReverser.App
{
    public class FileReverser : IFileReverser
    {
        private readonly IOutputter _outputter;
        private readonly IInput _input;
        private readonly IInputValidator _inputValidator;
        private readonly IOutputValidator _outputValidator;

        public const string InputMessage = "Please enter the full path and name of the input file: ";
        public const string OutputMessage = "Please enter the full path and name of the output file: ";

        public FileReverser(IOutputter outputter, IInput input, IInputValidator inputValidator, IOutputValidator outputValidator)
        {
            _outputter = outputter;
            _input = input;
            _inputValidator = inputValidator;
            _outputValidator = outputValidator;
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
            var result = _inputValidator.Validate(file);
            return result == 0 ? null : (int?)result;
        }

        public void PromptForOutput()
        {
            _outputter.Write(OutputMessage);
        }

        public int? ValidateOutput(string input)
        {
            var result = _outputValidator.Validate(input);
            return result == 0 ? null : (int?)result;
        }
    }
}