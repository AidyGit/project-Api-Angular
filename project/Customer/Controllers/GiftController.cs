using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using project.Customer.Dtos;
using project.Customer.Interfaces;
using project.Models.Customer;
using System.Security.Claims;

namespace project.Customer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //c-tor
    public class GiftController : ControllerBase
    {
        private readonly IGiftService _giftService;
        private readonly ILogger<GiftController> _logger;
        public GiftController(IGiftService giftService, ILogger<GiftController> logger)
        {
            _logger = logger;
            _giftService = giftService;
        }

        //Get all gifts
        [Authorize]
        [HttpGet("MyCart")]
        public async Task<ActionResult<IEnumerable<GiftDto.GiftDetailDto>>> GetMyCart()
        {
            foreach (var claim in User.Claims)
            {
                Console.WriteLine($"Type: {claim.Type}, Value: {claim.Value}");
            }
            //חילוץ שם משתמש מהטוקן
            var userName = User.FindFirst("unique_name")?.Value
                               ?? User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value
                               ?? User.Identity?.Name;
            if (string.IsNullOrEmpty(userName))
                return Unauthorized("User is not identified");

            try
            {
                var gifts = await _giftService.GetMyCart(userName);
                return Ok(gifts ?? Enumerable.Empty<GiftDto.GiftDetailDto>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting gifts");
                return BadRequest(ex.Message);
            }
        }

        public class UpdateCartRequest
        {
            public int giftId { get; set; }
            public string userName { get; set; }
            public int quantity { get; set; }
        }

        // Add to cart
        [Authorize]
        [HttpPost("AddToCart")]
        public async Task<ActionResult<bool>> AddGiftToCart([FromBody] UpdateCartRequest request)
        {
            try
            {
                var result = await _giftService.AddGiftToCart(request.giftId, request.userName, request.quantity);

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [Authorize]

        [HttpPut("AddOneToCart")]
        public async Task<ActionResult<bool>> AddOneToCart([FromQuery] int giftId,[FromBody] string userName)
        {
            try
            {
                var result = await _giftService.AddOneToCart(giftId, userName);

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }


        // Remove from cart
        [Authorize]
        [HttpDelete("RemoveFromCart")]
        public async Task<ActionResult<bool>> RemoveGiftFromCart([FromQuery] int giftId, [FromBody] string userName)
        {
            try
            {
                var result = await _giftService.RemoveGiftFromCart(giftId, userName);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        // Remove one from cart
        [Authorize]
        [HttpPut("RemoveOneFromCart")]
        public async Task<ActionResult<bool>> RemoveOne([FromQuery] int giftId, [FromBody] string userName)
        {
            try
            {
                var result = await _giftService.RemoveOne(giftId, userName);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        //[HttpPost("UpdateCart:id")]
        //public async Task<ActionResult<bool>> UpdateCart([FromQuery] int giftId, [FromBody] UpdateCartRequest request)
        //{
        //    try
        //    {
        //        var result = await _giftService.UpdateCart(giftId, request.userName, request.quantity);
        //        return Ok(result);
        //    }

        //    catch (ArgumentException ex)
        //    {
        //        throw new ArgumentException(ex.Message);
        //    }
        //}
        // Purchase and update cart status
        [Authorize]
        [HttpPut("UpdateStatusCart")]

        public async Task<ActionResult<bool>> UpdateStatusCart([FromQuery] int cartId)
        {
            try
            {
                var result = await _giftService.UpdateStatusCart(cartId);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

    }
}