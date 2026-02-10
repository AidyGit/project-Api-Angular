using project.Customer.Dtos;

namespace project.Customer.Interfaces
{
    public interface IGiftService
    {
        Task<IEnumerable<GiftDto.GiftDetailDto>> GetMyCart(string userName);
        Task<bool> AddGiftToCart(int giftId, string userName,int quantity);
        //Task<bool> RemoveGiftFromCart(int giftId, int userId);
        //Task<Models.Customer.ShoppingCartModel?> GetShoppingCartByUserId(int userId);
        Task<bool> RemoveGiftFromCart(int giftId, string userName);
        Task<bool> RemoveOne(int giftId, string userName);
        Task<bool> AddOneToCart(int giftId, string userName);
        //Task<bool> UpdateCart(int giftId, string userName,int quantity);
        Task<bool> UpdateStatusCart(int cartId);
    }
}
