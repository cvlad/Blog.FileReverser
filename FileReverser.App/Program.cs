using System.Reflection;
using Autofac;

namespace FileReverser.App
{
    /*
     * Requirements:
     * 
     * 1. The program should prompt for the input file.
     *      - the message should read "Please enter the full path and name of the input file: "
     * 2. The program should read a line from standard input which represents the full path to the file.
     *      - if the file does not exist, the program should return 1
     *      - if the file is not a text file, the program should return 2
     *      - if the program cannot open or read the file, it should return 3
     *      - the file can be empty, and the program should just output an empty file
     * 3. The program should prompt for the output file.
     *      - the message should read "Please enter the full path and name of the output file: "
     *      - if the file exists, the program should return 4
     *      - if the file cannot be created, the program should return 5
     * 4. The program should reverse the input file into the output file.
     *      - the program should return 0
     */
    static class Program
    {
        static int Main()
        {
            var container = SetupDependencyInjection();
            using (var scope = container.BeginLifetimeScope())
            {
                var fileReverser = scope.Resolve<IFileReverser>();

                fileReverser.PromptForInput();

                var inputFile = fileReverser.ReadInput();
                var validationResult = fileReverser.ValidateInput(inputFile);
                if (validationResult.HasValue)
                    return validationResult.Value;

                fileReverser.PromptForOutput();

                var outputFile = fileReverser.ReadInput();
                validationResult = fileReverser.ValidateOutput(outputFile);
                if (validationResult.HasValue)
                    return validationResult.Value;

                fileReverser.Reverse(inputFile, outputFile);

                return 0;
            }
        }

        private static IContainer SetupDependencyInjection()
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AsImplementedInterfaces();
            return builder.Build();
        }
    }
}
