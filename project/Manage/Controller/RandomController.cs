using Microsoft.AspNetCore.Mvc;
using project.Manage.Dtos;
using project.Manage.Interfaces;
using project.Manage.Function;

namespace project.Manage.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class RandomController : ControllerBase
    {
        private readonly IRandomService _randomService;
        private readonly ILogger _logger;

        public RandomController(IRandomService randomService,ILogger<RandomController> logger)
        {
            _randomService = randomService;
            _logger = logger;

        }

        [HttpGet("WinnerByDonation")]
        public async Task<ActionResult> GetWinnerByDonation()
        {
            try
            {
                // Ensure the result from GetWinnerToDonation is a List<RandomDto>
                var winners = await _randomService.GetWinnerToDonation();

                // Pass the correct type (List<RandomDto>) to DownloadWinnersExcel
                var excelResult = ExcelDownloadService.DownloadWinnersExcel(winners);

                // Use the FileContents property of FileContentResult to get the byte array
                return File(excelResult.FileContents, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Winners.xlsx");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
