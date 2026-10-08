using FileProcessingService.Models;

namespace FileProcessingService.Services
{
    public interface IFileProcessor
    {
        Task<FileProcessingResult> ProcessAsync(IFormFile file);
    }
}
