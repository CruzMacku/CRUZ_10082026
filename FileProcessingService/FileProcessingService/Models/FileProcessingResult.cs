namespace FileProcessingService.Models
{
    public class FileProcessingResult
    {
        public string FileName { get; set; } = string.Empty;
        public int Count { get; set; }
        public double Average { get; set; }
    }
}
