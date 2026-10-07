using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileProcessingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        // Post - CSV to receive and average the numbers in the CSV file and return the average as a response
        [HttpPost]
        public IActionResult PostCsv(IFormFile file)
        {
            try
            {

                if (file == null || file.Length == 0)
                {
                    return BadRequest("Please upload a file !");
                }

                string fileName = file.FileName;

                Console.WriteLine($"Processing : {fileName}...");

                var numbers = new List<int>();


                using var reader = new StreamReader(file.OpenReadStream());


                while (!reader.EndOfStream)
                {

                    var line = reader.ReadLine();


                    if (string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }

                    if (int.TryParse(line, out var number))
                    {
                        numbers.Add(number);
                    }
                }

                if (numbers.Count == 0)
                {
                    return BadRequest($"File {fileName} does not contain any numbers !");
                }



                int count = numbers.Count;

                double average = numbers.Average();

                Console.WriteLine("File Processed !");
                Console.WriteLine($"File name: {fileName} - Count: {count} - Average: {average}");


                return Ok(new
                {
                    fileName = fileName,
                    count = count,
                    average = average
                });

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while processing the file !");
            }

        }
    }
}
