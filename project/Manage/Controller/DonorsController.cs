using Microsoft.AspNetCore.Mvc;
using project.Customer.Interfaces;
using project.Manage.Dtos;
using project.Manage.Interfaces;
using project.Manage.Models;

namespace project.Manage.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class DonorsController : ControllerBase
    {
        private readonly IDonorsService _donorService;
        //c-tor

        public DonorsController(IDonorsService donorService)
        {
            _donorService = donorService;
        }

        [HttpGet("Donors")]
        public async Task<ActionResult<DonorsDto>> GetDonors()
        {
            var donors = await _donorService.GetDonors();
            return Ok(donors);
        }
        //AddDonor
        [HttpPost("AddDonor")]
        public async Task<ActionResult<bool>> AddDonor([FromQuery] DonorsDto donorDto)
        {
            try
            {
                return (Ok(await _donorService.AddDonor(donorDto)));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpDelete("DeleteDonor/{id}")]
        public async Task<ActionResult> DeleteDonor([FromQuery] int id)
        {
            var result = await _donorService.DeleteDonor(id);
            if (result)
            {
                return Ok(new { message = "Donor deleted successfully." });
            }
            else
            {
                return NotFound(new { message = "Donor not found." });
            }
        }
        //ApdateDonor
        [HttpPut("UpdateDonor/{id}")]
        public async Task<ActionResult<DonorsModel>> UpdateDonor([FromQuery] int id, DonorsUpdateDto donorToUp)
        {
            var donor = await _donorService.UpdateDonor(id, donorToUp);
            return Ok(donor);
        }

        public class DonorFilterParams
        {
            public string? Name { get; set; }
            public string? Email { get; set; }
            public string NameGift { get; set; }
        }
        //filter donors by name gift or email
        [HttpGet("FilterDonors")]
        public async Task<ActionResult<IEnumerable<DonorsDto>>> FilterDonors(DonorFilterParams DonorFilterParams)
        {
            var donors = await _donorService.FilterDonors(DonorFilterParams);
            return Ok(donors);
        }
    }
}
