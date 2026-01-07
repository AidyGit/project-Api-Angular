using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using project.Customer.Controllers;
using project.Manage.Dtos;
using project.Manage.Interfaces;
using project.Manage.Models;
using project.Manage.Services;
using System.Reflection.Metadata.Ecma335;

namespace project.Manage.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonationController : ControllerBase
    {
        private readonly IDonationService _donationService;
        private readonly ILogger<DonationController> _logger;

        //c-tor

        public DonationController(IDonationService donationService,ILogger<DonationController> logger)
        {
            _donationService = donationService;
            _logger = logger;
        }


        [HttpGet("Donations")]
        public async Task<ActionResult<IEnumerable<GetDonationDto>>> GetDonations()
        {
            try
            {
                var donations = await _donationService.GetDonations();
                return Ok(donations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [Authorize(Roles = "Admin")]
        //AddDonation
        [HttpPost("AddDonation")]
        public async Task<ActionResult<bool>> AddDonation([FromQuery] CreateDonationDto donationDto)
        {
            try
            {
                return (Ok(await _donationService.AddDonation(donationDto)));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        //DeleteDonation
        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteDonation/{id}")]
        public async Task<ActionResult> DeleteDonation([FromQuery] int id)
        {
            try
            {
                var result = await _donationService.DeleteDonation(id);
                if (result)
                {
                    return Ok(new { message = "Donor deleted successfully." });
                }
                else
                {
                    return NotFound(new { message = "Donor not found." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }
        //UpdateDonation
        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateDonation/{id}")]
        public async Task<ActionResult<DonorsModel>> UpdateDonation([FromQuery] int id, CreateDonationDto donorToUp)
        {
            try
            {
                var donor = await _donationService.UpdateDonation(id, donorToUp);
                return Ok(donor);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }


}
