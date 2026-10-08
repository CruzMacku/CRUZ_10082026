namespace FileProcessingService.Models
{
    public class ProcessedFile
    {
        public string FileName { get; set; } = string.Empty;
        public int Count { get; set; }
        public double Average { get; set; }
        public DateTime ProcessedAt { get; set; }
    }
}
