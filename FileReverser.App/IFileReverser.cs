namespace FileReverser.App
{
    public interface IFileReverser
    {
        void PromptForInput();
        string ReadInput();
        int? ValidateInput(string file);
    }
}