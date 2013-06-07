namespace FileReverser.App
{
    public class FileReverser
    {
        private readonly IOutputter _outputter;

        public const string InputMessage = "Please enter the full path and name of the input file: ";

        public FileReverser(IOutputter outputter)
        {
            _outputter = outputter;
        }

        public void PromptForInput()
        {
            _outputter.Write(InputMessage);
        }
    }
}