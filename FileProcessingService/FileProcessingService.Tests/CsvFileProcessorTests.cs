using FileProcessingService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging.Abstractions;
using System.Text;

namespace FileProcessingService.Tests
{
    public class CsvFileProcessorTests
    {



        [Fact]
        public async Task ProcessAsync_ReturnsCorrectAverage()
        {
            var processor = new CsvFileProcessor(
                NullLogger<CsvFileProcessor>.Instance);

            var csv = """
                        Name,Score
                        Name1,1
                        Name2,2
                        Name3,3
                        Name4,4
                        Name5,5
                       """;
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(csv));
            var file = new FormFile(stream, 0, stream.Length, "file", "test.csv");

            var result = await processor.ProcessAsync(file);

            Assert.Equal(5, result.Count);
            Assert.Equal(3, result.Average);
        }

        [Fact]
        public async Task ProcessAsync_HandlesDecimals()
        {
            var processor = new CsvFileProcessor(
                NullLogger<CsvFileProcessor>.Instance);

            var csv = """
                        Name,Score
                        Name1,1
                        Name2,2
                        Name3,3.5
                        Name4,4
                        Name5,5
                       """;
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(csv));
            var file = new FormFile(stream, 0, stream.Length, "file", "test.csv");

            var result = await processor.ProcessAsync(file);

            Assert.Equal(5, result.Count);
            Assert.Equal(3.1, result.Average);
        }
    }
}
