using FileProcessingService.Models;
using FileProcessingService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FileProcessingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFileProcessor _fileProcessor;
        private readonly ILogger<CsvFileProcessor> _logger;

        private readonly FileTrackingService _fileTrackingService;

        public FilesController(IFileProcessor fileProcessor, ILogger<CsvFileProcessor> logger, FileTrackingService fileTrackingService)
        {
            _fileProcessor = fileProcessor;
            _logger = logger;
            _fileTrackingService = fileTrackingService;
        }

        // Post - CSV to receive and average the numbers in the CSV file and return the average as a response
        [HttpPost("process-csv")]
        public async Task<IActionResult> ProcessCSV(IFormFile file)
        {
            try
            {
                //Process the file
                var result = await _fileProcessor.ProcessAsync(file);

                //Record the processed file
                _fileTrackingService.Add(new ProcessedFile
                {
                    FileName = file.FileName,
                    Count = result.Count,
                    Average = result.Average,
                    ProcessedAt = DateTime.UtcNow
                });

                return Ok(result);

            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to process the file");
                return StatusCode(500, "Failed to process the file");
            }
        }

        [HttpGet("report")]
        public IActionResult GetReport()
        {
            return Ok(_fileTrackingService.GetAll());
        }
    }
}
