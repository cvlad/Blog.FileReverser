namespace FileReverser.App
{
    public interface IFileReverser
    {
        void PromptForInput();
        string ReadInput();
        int? ValidateInput(string file);
        void PromptForOutput();
        int? ValidateOutput(string file);
        void Reverse(string inputFile, string outputFile);
    }
}