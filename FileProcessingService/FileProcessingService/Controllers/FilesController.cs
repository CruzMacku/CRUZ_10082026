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

        public FilesController(IFileProcessor fileProcessor)
        {
            _fileProcessor = fileProcessor;
        }

        // Post - CSV to receive and average the numbers in the CSV file and return the average as a response
        [Route("process-csv")]
        [HttpPost]
        public async Task<IActionResult> ProcessCSV(IFormFile file)
        {
            try
            {
                var result = await _fileProcessor.ProcessAsync(file);
                return Ok(result);

            }
            catch (Exception ex)
            {
                // return status 500

                throw ex;
            }
        }
    }
}
