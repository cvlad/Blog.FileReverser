using FileReverser.App;
using Moq;
using Xunit;

namespace FileReverser.Facts
{
    public class FileReverserFacts
    {
        [Fact]
        public void FileReverser_prompts_for_input_file_with_correct_message()
        {
            // Given
            var outputterMock = new Mock<IOutputter>();
            outputterMock.Setup(o => o.Write(App.FileReverser.InputMessage)).Verifiable();
            var fileReverser = new App.FileReverser(outputterMock.Object, null);

            // When
            fileReverser.PromptForInput();

            // Then
            outputterMock.Verify();
        }

        [Fact]
        public void FileReverser_reads_file_input()
        {
            // Given
            var inputMock = new Mock<IInput>();
            inputMock.Setup(i => i.Read()).Verifiable();
            var fileReverser = new App.FileReverser(null, inputMock.Object);

            // When
            fileReverser.ReadInput();

            // Then
            inputMock.Verify();
        }
    }
}
