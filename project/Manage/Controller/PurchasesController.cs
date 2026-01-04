using Microsoft.AspNetCore.Mvc;
using project.Manage.Dtos;
using project.Manage.Interfaces;

namespace project.Manage.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasesController : ControllerBase
    {
        private readonly IPurchasesService _puchasesService;
        public PurchasesController(IPurchasesService puchasesService)
        {
            _puchasesService = puchasesService;
        }

        [HttpGet("Puchases/{id}")]
        public async Task<ActionResult<GetDonationWithPurchase>> GetPuchasesByDonation([FromQuery] int id)
        {
            try
            {
                return Ok(await _puchasesService.GetPuchasesByDonation(id));

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet("Purchases")]
        public async Task<ActionResult<GetDonationWithPurchase>> GetPurchases()
        {
            try
            {
                return Ok(await _puchasesService.GetPuchases());

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
