using System;
using System.Linq;
using FileReverser.App;
using Moq;
using Xunit;

namespace FileReverser.Facts
{
    public class ReverserImplementationFacts
    {
        [Fact]
        public void ReverserImplementation_reverse_reads_input_file()
        {
            // Given
            var fileReaderMock = new Mock<IFileReader>();
            fileReaderMock.Setup(fr => fr.Read(It.IsAny<string>())).Returns(string.Empty).Verifiable();

            var fileWriterStub = new Mock<IFileWriter>();
            fileWriterStub.Setup(fw => fw.Write(It.IsAny<string>(), It.IsAny<string>()));

            var reverser = new ReverserImplementation(fileReaderMock.Object, fileWriterStub.Object);

            // When
            reverser.Reverse(string.Empty, string.Empty);

            // Then
            fileReaderMock.Verify();
        }

        [Fact]
        public void ReverserImplementation_reverse_writes_reversed_input_to_output_file()
        {
            // Given
            var content = Guid.NewGuid().ToString();
            var expected = new string(content.Reverse().ToArray());

            var fileReaderStub = new Mock<IFileReader>();
            fileReaderStub.Setup(fr => fr.Read(It.IsAny<string>())).Returns(content);

            var fileWriterMock = new Mock<IFileWriter>();
            fileWriterMock.Setup(fw => fw.Write(It.IsAny<string>(), expected)).Verifiable();

            var reverser = new ReverserImplementation(fileReaderStub.Object, fileWriterMock.Object);

            // When
            reverser.Reverse(string.Empty, string.Empty);

            // Then
            fileWriterMock.Verify();
        }
    }
}
