namespace FileReverser.App
{
    public class ConsoleInput : IInput
    {
        public string Read()
        {
            return System.Console.ReadLine();
        }
    }
}