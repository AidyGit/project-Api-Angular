using project.Customer.Dtos;
using project.Manage.Models;
using project.Models.Customer;

namespace project.Customer.Interfaces
{
    public interface IGiftRepository
    {
        Task<IEnumerable<GiftDto.GiftDetailDto>> GetMyCart(int userId);
        //Task<bool> AddGiftToCart(int giftId, string userName);
        Task<DonationsModel> FindGiftById(int giftId);
        Task CreateShoppingCart(ShoppingCartModel newCart);
        Task<bool> SaveChangesInShoppingCard();
        Task<bool> RemoveGiftFromCart(int giftId,int userId);
        Task<bool> RemoveOne(int giftId, int userId);
        Task<bool> AddOneToCart(int giftId, int userId);
        Task<ShoppingCartModel> GetOrCreateActiveCart(int userId);
        Task<bool> UpdateStatusCart(int cartId);
    }
}
