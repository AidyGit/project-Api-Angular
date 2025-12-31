using Microsoft.AspNetCore.Mvc;
using project.Customer.Interfaces;
using project.Manage.Dtos;
using project.Manage.Interfaces;

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
        public async Task<ActionResult<IEnumerable<DonorsDto>>> GetDonors()
        {
            var donors = await _donorService.GetDonors();
            return Ok(donors);
        }
    }
}
