using Microsoft.AspNetCore.Mvc;
using project.Customer.Dtos;
using project.Customer.Interfaces;
using project.Models.Customer;

namespace project.Customer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //c-tor
    public class GiftController : ControllerBase
    {
        private readonly IGiftService _giftService;

        public GiftController(IGiftService giftService)
        {
            _giftService = giftService;
        }

        //Get all gifts
        [HttpGet("Gifts")]
        public async Task<IEnumerable<GiftDto.GiftDetailDto>> GetAllGifts()
        {
            try
            {
                var gifts = await _giftService.GetGifts();

                // Ensure the return type matches IEnumerable<GiftDto.GiftDetailDto>
                if (gifts == null)
                {
                    return Enumerable.Empty<GiftDto.GiftDetailDto>();
                }

                return gifts;
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
        public class UpdateCartRequest
        {
            public string userName { get; set; }
            public int quantity { get; set; }
        }

        // Add to cart
        [HttpPost("AddToCart:id")]
        public async Task<ActionResult<bool>> AddGiftToCart([FromQuery] int giftId, [FromBody] UpdateCartRequest request)
        {
            try
            {
                var result = await _giftService.AddGiftToCart(giftId, request.userName, request.quantity);

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        [HttpPost("AddOneToCart:id")]
        public async Task<ActionResult<bool>> AddOneToCart([FromQuery] int giftId, string userName)
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
        [HttpDelete("RemoveFromCart:id")]
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
        [HttpDelete("RemoveOneFromCart:id")]
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

        [HttpPut("UpdateStatusCart:id")]

        public async Task<ActionResult<bool>> UpdateStatusCart([FromQuery] int cartId, int quantity)
        {
            try
            {
                var result = await _giftService.UpdateStatusCart(cartId, quantity);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

    }
}