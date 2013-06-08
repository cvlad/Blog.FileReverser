namespace FileReverser.App
{
    public class FileReverser
    {
        private readonly IOutputter _outputter;
        private readonly IInput _input;

        public const string InputMessage = "Please enter the full path and name of the input file: ";

        public FileReverser(IOutputter outputter, IInput input)
        {
            _outputter = outputter;
            _input = input;
        }

        public void PromptForInput()
        {
            _outputter.Write(InputMessage);
        }

        public string ReadInput()
        {
            return _input.Read();
        }
    }
}