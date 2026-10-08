using FileProcessingService.Models;

namespace FileProcessingService.Services
{
    public class FileTrackingService
    {
        private readonly List<ProcessedFile> _files = new List<ProcessedFile>();

        public void Add(ProcessedFile file)
        {
            _files.Add(file);
        }

        public List<ProcessedFile> GetAll()
        {
            return _files;
        }
    }
}
