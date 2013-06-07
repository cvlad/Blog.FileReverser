using FileReverser.App;
using Moq;
using Xunit;

namespace FileReverser.Facts
{
    public class PromptForInputFile
    {
        [Fact]
        public void FileReverser_prompts_for_input_file_with_correct_message()
        {
            // Given
            var outputterMock = new Mock<IOutputter>();
            outputterMock.Setup(o => o.Write(App.FileReverser.InputMessage)).Verifiable();
            var fileReverser = new App.FileReverser(outputterMock.Object);

            // When
            fileReverser.PromptForInput();

            // Then
            outputterMock.Verify();
        }
    }
}
