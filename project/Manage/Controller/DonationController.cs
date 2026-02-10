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

        public DonationController(IDonationService donationService, ILogger<DonationController> logger)
        {
            _donationService = donationService;
            _logger = logger;
        }

        //GetDonations
        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetDonationDto>>> GetDonations()
        {
            try
            {
                var donations = await _donationService.GetDonations();
                return Ok(donations);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching donations"); // CHANGED: הוספת לוג
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //AddDonation
        [Authorize(Roles = "Admin")]
        [HttpPost("AddDonation")]
        public async Task<ActionResult<bool>> AddDonation([FromBody] CreateDonationDto donationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return (Ok(await _donationService.AddDonation(donationDto)));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding donation"); // CHANGED: הוספת לוג
                return BadRequest(new { message = ex.Message });
            }
        }
        //DeleteDonation
        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteDonation/{id}")]
        public async Task<ActionResult> DeleteDonation(int id)
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
                _logger.LogError(ex, "Error deleting donation"); // CHANGED: הוספת לוג
                return BadRequest(new { message = ex.Message });
            }

        }
        //UpdateDonation
        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateDonation/{id}")]
        public async Task<ActionResult<DonorsModel>> UpdateDonation(int id, [FromBody] CreateDonationDto donorToUp)
        {
            try
            {
                if (donorToUp == null) return BadRequest();
                var donor = await _donationService.UpdateDonation(id, donorToUp);
                return Ok(donor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating donation"); // CHANGED: הוספת לוג
                return BadRequest(new { message = ex.Message });
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("FilterDonations")]
        public async Task<ActionResult<IEnumerable<GetDonationWithPurchase>>> SearchDonations(
        [FromQuery] string? donationName,
        [FromQuery] string? donorName,
        [FromQuery] int? minPurchases)
        {
            try
            {
                var results = await _donationService.SearchDonations(donationName, donorName, minPurchases);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching donations");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("GetIdByEmail")]
        public async Task<ActionResult<int>> getUserIdByEmail([FromQuery] string email)
        {
            var id = await _donationService.GetIdByEmail(email);
            return Ok(id);
        }

        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<CategoryModel>>> GetAllCategories()
        {
            var category = await _donationService.GetAllCategories();
            return Ok(category);
        }
    }
}