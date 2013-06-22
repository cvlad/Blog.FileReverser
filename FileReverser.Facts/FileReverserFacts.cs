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
            var fileReverser = new App.FileReverser(outputterMock.Object, null, null, null, null);

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
            var fileReverser = new App.FileReverser(null, inputMock.Object, validatorStub.Object, null, null);

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
            var fileReverser = new App.FileReverser(null, null, validatorMock.Object, null, null);

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
        public void FileReverser_validate_input_forwards_validator_result(int validatorResult, int? expected)
        {
            // Given
            var validatorStub = new Mock<IInputValidator>();
            validatorStub.Setup(v => v.Validate(It.IsAny<string>())).Returns(validatorResult);
            var fileReverser = new App.FileReverser(null, null, validatorStub.Object, null, null);

            // When
            var result = fileReverser.ValidateInput(string.Empty);

            // Then
            result.Should().Be(expected);
        }

        [Fact]
        public void FileReverser_prompts_for_output_file_with_correct_message()
        {
            // Given
            var outputterMock = new Mock<IOutputter>();
            outputterMock.Setup(o => o.Write(App.FileReverser.OutputMessage)).Verifiable();
            var fileReverser = new App.FileReverser(outputterMock.Object, null, null, null, null);

            // When
            fileReverser.PromptForOutput();

            // Then
            outputterMock.Verify();
        }

        [Fact]
        public void FileReverser_validates_file_output()
        {
            // Given
            var validatorMock = new Mock<IOutputValidator>();
            validatorMock.Setup(v => v.Validate(It.IsAny<string>())).Verifiable();
            var fileReverser = new App.FileReverser(null, null, null, validatorMock.Object, null);

            // When
            fileReverser.ValidateOutput(string.Empty);

            // Then
            validatorMock.Verify();
        }

        [Theory]
        [InlineData(0, null)]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        public void FileReverser_validate_output_forwards_validator_result(int validatorResult, int? expected)
        {
            // Given
            var validatorStub = new Mock<IOutputValidator>();
            validatorStub.Setup(v => v.Validate(It.IsAny<string>())).Returns(validatorResult);
            var fileReverser = new App.FileReverser(null, null, null, validatorStub.Object, null);

            // When
            var result = fileReverser.ValidateOutput(string.Empty);

            // Then
            result.Should().Be(expected);
        }

        [Fact]
        public void FileReverser_should_use_the_reverser_implementation()
        {
            // Given
            var reverserImplMock = new Mock<IReverserImplementation>();
            reverserImplMock.Setup(r => r.Reverse(It.IsAny<string>(), It.IsAny<string>())).Verifiable();
            var fileReverser = new App.FileReverser(null, null, null, null, reverserImplMock.Object);

            // When
            fileReverser.Reverse(string.Empty, string.Empty);

            // Then
            reverserImplMock.Verify();
        }
    }
}
