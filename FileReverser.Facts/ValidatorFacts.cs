using FileReverser.App;
using FluentAssertions;
using Moq;
using Xunit;
using Xunit.Extensions;

namespace FileReverser.Facts
{
    public class ValidatorFacts
    {
        [Theory]
        [InlineData("")]
        [InlineData(@"some\path")]
        public void Input_validator_passes_the_correct_filename(string fileName)
        {
            // Given
            var fileCheckerMock = new Mock<IFileChecker>();
            fileCheckerMock.Setup(fc => fc.FileExists(fileName)).Returns(true).Verifiable();

            var textFileCheckerMock = new Mock<ITextFileChecker>();
            textFileCheckerMock.Setup(tfc => tfc.IsTextFile(fileName)).Returns(true).Verifiable();

            var permissionCheckerMock = new Mock<IPermissionChecker>();
            permissionCheckerMock.Setup(pc => pc.CanReadFile(fileName)).Verifiable();

            var validator = new InputFileValidator(fileCheckerMock.Object, textFileCheckerMock.Object, permissionCheckerMock.Object);

            // When
            validator.Validate(fileName);

            // Then
            fileCheckerMock.Verify();
            textFileCheckerMock.Verify();
            permissionCheckerMock.Verify();
        }

        [Fact]
        public void Input_validator_returns_0_if_all_validators_pass()
        {
            // Given
            var fileName = string.Empty;

            var fileCheckerStub = new Mock<IFileChecker>();
            fileCheckerStub.Setup(fc => fc.FileExists(fileName)).Returns(true);

            var textFileCheckerStub = new Mock<ITextFileChecker>();
            textFileCheckerStub.Setup(tfc => tfc.IsTextFile(fileName)).Returns(true).Verifiable();

            var permissionsCheckerStub = new Mock<IPermissionChecker>();
            permissionsCheckerStub.Setup(pc => pc.CanReadFile(fileName)).Returns(true).Verifiable();

            var validator = new InputFileValidator(fileCheckerStub.Object, textFileCheckerStub.Object, permissionsCheckerStub.Object);

            // When
            var result = validator.Validate(fileName);

            // Then
            result.Should().Be(0);
        }

        [Fact]
        public void Input_validator_returns_1_if_file_does_not_exist()
        {
            // Given
            var fileName = string.Empty;
            var fileCheckerStub = new Mock<IFileChecker>();
            fileCheckerStub.Setup(fc => fc.FileExists(fileName)).Returns(false);
            var validator = new InputFileValidator(fileCheckerStub.Object, null, null);

            // When
            var result = validator.Validate(fileName);

            // Then
            result.Should().Be(1);
        }

        [Fact]
        public void Input_validator_returns_2_if_file_is_not_textFile()
        {
            // Given
            var fileName = string.Empty;

            var fileCheckerStub = new Mock<IFileChecker>();
            fileCheckerStub.Setup(fc => fc.FileExists(fileName)).Returns(true);

            var textFileCheckerStub = new Mock<ITextFileChecker>();
            textFileCheckerStub.Setup(tfc => tfc.IsTextFile(fileName)).Returns(false);

            var validator = new InputFileValidator(fileCheckerStub.Object, textFileCheckerStub.Object, null);

            // When
            var result = validator.Validate(fileName);

            // Then
            result.Should().Be(2);
        }

        [Fact]
        public void Input_validator_returns_3_if_file_is_not_textFile()
        {
            // Given
            var fileName = string.Empty;

            var fileCheckerStub = new Mock<IFileChecker>();
            fileCheckerStub.Setup(fc => fc.FileExists(fileName)).Returns(true);

            var textFileCheckerStub = new Mock<ITextFileChecker>();
            textFileCheckerStub.Setup(tfc => tfc.IsTextFile(fileName)).Returns(true);

            var permissionsCheckerStub = new Mock<IPermissionChecker>();
            permissionsCheckerStub.Setup(pc => pc.CanReadFile(fileName)).Returns(false);

            var validator = new InputFileValidator(fileCheckerStub.Object, textFileCheckerStub.Object, permissionsCheckerStub.Object);

            // When
            var result = validator.Validate(fileName);

            // Then
            result.Should().Be(3);
        }

        [Theory]
        [InlineData("")]
        [InlineData(@"some\path")]
        public void Output_validator_passes_the_correct_filename(string fileName)
        {
            // Given
            var fileCheckerMock = new Mock<IFileChecker>();
            fileCheckerMock.Setup(fc => fc.FileExists(fileName)).Returns(false).Verifiable();

            var fileCreateMock = new Mock<IFileCreate>();
            fileCreateMock.Setup(fc => fc.CanCreate(fileName)).Returns(true).Verifiable();

            var validator = new OutputFileValidator(fileCheckerMock.Object, fileCreateMock.Object);

            // When
            validator.Validate(fileName);

            // Then
            fileCheckerMock.Verify();
            fileCreateMock.Verify();
        }

        [Fact]
        public void Output_validator_returns_0_if_all_validators_pass()
        {
            // Given
            var fileName = string.Empty;
            var fileCheckerStub = new Mock<IFileChecker>();
            fileCheckerStub.Setup(fc => fc.FileExists(fileName)).Returns(false);

            var fileCreateStub = new Mock<IFileCreate>();
            fileCreateStub.Setup(fc => fc.CanCreate(fileName)).Returns(true);

            var validator = new OutputFileValidator(fileCheckerStub.Object, fileCreateStub.Object);

            // When
            var result = validator.Validate(fileName);

            // Then
            result.Should().Be(0);
        }

        [Fact]
        public void Output_validator_returns_4_if_file_does_exists()
        {
            // Given
            var fileName = string.Empty;

            var fileCheckerStub = new Mock<IFileChecker>();
            fileCheckerStub.Setup(fc => fc.FileExists(fileName)).Returns(true);

            var validator = new OutputFileValidator(fileCheckerStub.Object, null);

            // When
            var result = validator.Validate(fileName);

            // Then
            result.Should().Be(4);
        }

        [Fact]
        public void Output_validator_returns_5_if_file_cannot_be_created()
        {
            // Given
            var fileName = string.Empty;

            var fileCheckerStub = new Mock<IFileChecker>();
            fileCheckerStub.Setup(fc => fc.FileExists(fileName)).Returns(false);

            var fileCreateStub = new Mock<IFileCreate>();
            fileCreateStub.Setup(fc => fc.CanCreate(fileName)).Returns(false);

            var validator = new OutputFileValidator(fileCheckerStub.Object, fileCreateStub.Object);

            // When
            var result = validator.Validate(fileName);

            // Then
            result.Should().Be(5);
        }
    }
}
