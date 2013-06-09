using FileReverser.App;
using FluentAssertions;
using Moq;
using Xunit;
using Xunit.Extensions;

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
            var fileReverser = new App.FileReverser(outputterMock.Object, null, null);

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
            var validatorStub = new Mock<IInputValidator>();
            var fileReverser = new App.FileReverser(null, inputMock.Object, validatorStub.Object);

            // When
            fileReverser.ReadInput();

            // Then
            inputMock.Verify();
        }

        [Fact]
        public void FileReverser_validates_file_input()
        {
            // Given
            var validatorMock = new Mock<IInputValidator>();
            validatorMock.Setup(v => v.Validate(It.IsAny<string>())).Verifiable();
            var fileReverser = new App.FileReverser(null, null, validatorMock.Object);

            // When
            fileReverser.ValidateInput(string.Empty);

            // Then
            validatorMock.Verify();
        }

        [Theory]
        [InlineData(0, null)]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        public void FileReverser_validate_forwards_validator_result(int validatorResult, int? expected)
        {
            // Given
            var validatorMock = new Mock<IInputValidator>();
            validatorMock.Setup(v => v.Validate(It.IsAny<string>())).Returns(validatorResult).Verifiable();
            var fileReverser = new App.FileReverser(null, null, validatorMock.Object);

            // When
            var result = fileReverser.ValidateInput(string.Empty);

            // Then
            result.Should().Be(expected);
        }
    }
}
