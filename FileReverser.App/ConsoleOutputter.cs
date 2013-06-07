namespace FileReverser.App
{
    public class ConsoleOutputter : IOutputter
    {
        public void Write(string message)
        {
            System.Console.Write(message);
        }
    }
}
