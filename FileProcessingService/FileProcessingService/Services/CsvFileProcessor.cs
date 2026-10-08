using FileProcessingService.Models;

namespace FileProcessingService.Services
{
    public class CsvFileProcessor : IFileProcessor
    {

        private readonly ILogger<CsvFileProcessor> _logger;
        public CsvFileProcessor(ILogger<CsvFileProcessor> logger)
        {
            _logger = logger;
        }

        public Task<FileProcessingResult> ProcessAsync(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    throw new ArgumentException("Please upload a file.");
                }

                if (Path.GetExtension(file.FileName).ToLower() != ".csv")
                {
                    throw new ArgumentException("Only CSV files are supported.");
                }

                string fileName = file.FileName;

                _logger.LogInformation($"Processing : {fileName}...");

                var numbers = new List<double>();

                using var reader = new StreamReader(file.OpenReadStream());

                while (!reader.EndOfStream)
                {

                    var line = reader.ReadLine();


                    if (string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }

                    if (double.TryParse(line, out var number))
                    {
                        numbers.Add(number);
                    }
                }

                if (numbers.Count == 0)
                {
                    throw new ArgumentException($"File {fileName} does not contain any numbers !");
                }

                int count = numbers.Count;

                double average = numbers.Average();

                _logger.LogInformation("File Processed !");
                _logger.LogInformation($"File name: {fileName} - Count: {count} - Average: {average}");

                return Task.FromResult(new FileProcessingResult
                {
                    FileName = file.FileName,
                    Count = numbers.Count,
                    Average = numbers.Average()
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new ApplicationException("Error while processing the file !", ex);
            }

        }
    }
}
