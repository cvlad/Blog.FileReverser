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
        public void Validator_passes_the_correct_filename(string fileName)
        {
            // Given
            var fileCheckerMock = new Mock<IFileChecker>();
            fileCheckerMock.Setup(fc => fc.FileExists(fileName)).Returns(true).Verifiable();

            var textFileCheckerMock = new Mock<ITextFileChecker>();
            textFileCheckerMock.Setup(tfc => tfc.IsTextFile(fileName)).Returns(true).Verifiable();

            var permissionCheckerMock = new Mock<IPermissionChecker>();
            permissionCheckerMock.Setup(pc => pc.CanReadFile(fileName)).Verifiable();

            var validator = new InputFileValidator(fileCheckerMock.Object, textFileCheckerMock.Object, permissionCheckerMock.Object, fileName);

            // When
            validator.Validate();

            // Then
            fileCheckerMock.Verify();
            textFileCheckerMock.Verify();
            permissionCheckerMock.Verify();
        }

        [Fact]
        public void Validator_returns_0_if_all_validators_pass()
        {
            // Given
            var fileName = string.Empty;

            var fileCheckerStub = new Mock<IFileChecker>();
            fileCheckerStub.Setup(fc => fc.FileExists(fileName)).Returns(true);

            var textFileCheckerStub = new Mock<ITextFileChecker>();
            textFileCheckerStub.Setup(tfc => tfc.IsTextFile(fileName)).Returns(true).Verifiable();

            var permissionsCheckerStub = new Mock<IPermissionChecker>();
            permissionsCheckerStub.Setup(pc => pc.CanReadFile(fileName)).Returns(true).Verifiable();

            var validator = new InputFileValidator(fileCheckerStub.Object, textFileCheckerStub.Object, permissionsCheckerStub.Object, fileName);

            // When
            var result = validator.Validate();

            // Then
            result.Should().Be(0);
        }

        [Fact]
        public void Validator_returns_1_if_file_does_not_exist()
        {
            // Given
            var fileName = string.Empty;
            var fileCheckerStub = new Mock<IFileChecker>();
            fileCheckerStub.Setup(fc => fc.FileExists(fileName)).Returns(false);
            var validator = new InputFileValidator(fileCheckerStub.Object, null, null, fileName);

            // When
            var result = validator.Validate();

            // Then
            result.Should().Be(1);
        }

        [Fact]
        public void Validator_returns_2_if_file_is_not_textFile()
        {
            // Given
            var fileName = string.Empty;

            var fileCheckerStub = new Mock<IFileChecker>();
            fileCheckerStub.Setup(fc => fc.FileExists(fileName)).Returns(true);

            var textFileCheckerStub = new Mock<ITextFileChecker>();
            textFileCheckerStub.Setup(tfc => tfc.IsTextFile(fileName)).Returns(false).Verifiable();

            var validator = new InputFileValidator(fileCheckerStub.Object, textFileCheckerStub.Object, null, fileName);

            // When
            var result = validator.Validate();

            // Then
            result.Should().Be(2);
        }

        [Fact]
        public void Validator_returns_3_if_file_is_not_textFile()
        {
            // Given
            var fileName = string.Empty;

            var fileCheckerStub = new Mock<IFileChecker>();
            fileCheckerStub.Setup(fc => fc.FileExists(fileName)).Returns(true);

            var textFileCheckerStub = new Mock<ITextFileChecker>();
            textFileCheckerStub.Setup(tfc => tfc.IsTextFile(fileName)).Returns(true).Verifiable();

            var permissionsCheckerStub = new Mock<IPermissionChecker>();
            permissionsCheckerStub.Setup(pc => pc.CanReadFile(fileName)).Returns(false).Verifiable();

            var validator = new InputFileValidator(fileCheckerStub.Object, textFileCheckerStub.Object, permissionsCheckerStub.Object, fileName);

            // When
            var result = validator.Validate();

            // Then
            result.Should().Be(3);
        }
    }
}
