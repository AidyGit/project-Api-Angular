using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using project.Manage.Dtos;
using project.Manage.Interfaces;
using project.Manage.Models;

namespace project.Manage.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasesController : ControllerBase
    {
        private readonly IPurchasesService _puchasesService;
        private readonly ILogger<PurchasesController> _logger;
        public PurchasesController(IPurchasesService puchasesService,ILogger<PurchasesController> logger)
        {
            _puchasesService = puchasesService;
            _logger = logger;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Purchases/{id}")]
        public async Task<ActionResult<IEnumerable< PurchasesDto>>> GetPuchasesByDonation(int id)
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

        [Authorize(Roles = "Admin")]
        [HttpGet("Purchases")]
        public async Task<ActionResult<IEnumerable<PurchasesDto>>> GetPurchases()
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

        [Authorize(Roles = "Admin")]
        [HttpGet("PurchasesBySort")]
        public async Task<ActionResult<IEnumerable<PurchasesDto>>> GetPurchasesBySort([FromQuery] string sortBy)
        {
            try
            {
                return Ok(await _puchasesService.GetPurchasesBySort(sortBy));

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }



        [Authorize(Roles = "Admin")]
        [HttpGet("download-revenue")]
        public async Task<IActionResult> Download()
        {
            try
            {
                // קריאה לסרביס שמייצר את הקובץ
                var fileBytes = await _puchasesService.GetRevenueExcelFileAsync();

                if (fileBytes == null || fileBytes.Length == 0)
                    return NotFound("אין נתונים להפקה");

                // החזרת הקובץ להורדה
                return File(
                    fileBytes,
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    "RevenueReport.xlsx"
                );
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
