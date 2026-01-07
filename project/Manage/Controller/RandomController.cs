using Microsoft.AspNetCore.Mvc;
using project.Manage.Dtos;
using project.Manage.Interfaces;

namespace project.Manage.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class RandomController : ControllerBase
    {
        private readonly IRandomService _randomService;
        public RandomController(IRandomService randomService)
        {
            _randomService = randomService;
        }


        [HttpGet("WinnerByDonation")]
        public async Task<ActionResult<RandomDto>> GetWinnerByDonation()
        {
            try
            {
                return Ok(await _randomService.GetWinnerToDonation());

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        // [HttpGet("DownloadWinnersExcel")]
        // public async Task<IActionResult> DownloadWinnersExcel()
        // {
        //     try
        //     {
        //         var winners = await _randomService.GetWinners();
        //         return await _randomService.DownloadWinnersExcel(winners);
        //     }
        //     catch (Exception ex)
        //     {
        //         return BadRequest(new { message = ex.Message });
        //     }
        // }
    }
}
