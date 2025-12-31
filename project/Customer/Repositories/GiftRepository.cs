using Microsoft.EntityFrameworkCore;
using project.Customer.Dtos;
using project.Customer.Interfaces;
using project.Data;
using project.Manage.Models;
using project.Models.Customer;
using System.Linq;
namespace project.Customer.Repositories
{
    public class GiftRepository : IGiftRepository
    {
        private readonly ApplicationDbContext _context;

        public GiftRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<UserModel?> GetUserByUserName(string userName)
        {
            return await _context.UserModel
                .Include(u => u.ShoppingCarts) // טוען את הסל
                    .ThenInclude(sc => sc.GiftShoppingCart) // טוען את המוצרים שבתוך הסל
                .FirstOrDefaultAsync(u => u.UserName == userName);
        }
        public async Task<ShoppingCartModel> GetOrCreateActiveCart(int userId)
        {
            // (1. ניסיון למצוא סל קיים שעדיין פעיל (סטטוס 0)
            var activeCart = await _context.ShoppingCartModel
                .Include(c => c.GiftShoppingCart) // טעינת המוצרים שבתוך הסל
                .FirstOrDefaultAsync(c => c.UserId == userId && c.Status == CartStatus.Draft);

            // 2. אם לא נמצא סל כזה, ניצור אחד חדש
            if (activeCart == null)
            {
                activeCart = new ShoppingCartModel
                {
                    UserId = userId,
                    Status = CartStatus.Draft, // הגדרה כסל פעיל
                    GiftShoppingCart = new List<GiftShoppingCartModel>() // אתחול הרשימה
                };

                await _context.ShoppingCartModel.AddAsync(activeCart);
                await _context.SaveChangesAsync(); // שמירה כדי שהסל יקבל ID מהמסד נתונים
            }

            return activeCart;
        }
        public async Task<IEnumerable<GiftDto.GiftDetailDto>> GetGifts()
        {
            return await _context.DonationsModel.Include(d => d.Category).Select(u => new GiftDto.GiftDetailDto
            {
                Description = u.Description,
                Name = u.Name,
                Category = u.CategoryId,
                PriceTiket = u.PriceTiket,
                ImageUrl = u.ImageUrl
            })
            .OrderBy(g => g.PriceTiket)
            .ThenBy(g => g.Category)
            .ToListAsync();
        }

        //func to service to save changes in sql
        public async Task CreateShoppingCart(ShoppingCartModel newCart)
        {
            await _context.ShoppingCartModel.AddAsync(newCart);
        }

        //func to service find the gift with id
        public async Task<DonationsModel?> FindGiftById(int giftId)
        {
            return await _context.DonationsModel.FindAsync(giftId);
        }

        public async Task<bool> SaveChangesInShoppingCard()
        {
            var res = await _context.SaveChangesAsync();
            return res > 0;
        }

        //remove gift from cart
        public async Task<bool> RemoveGiftFromCart(int giftId, int userId)
        {
            var giftToRemove = await _context.GiftShoppingCartModel
                .Where(g => g.DonationId == giftId && g.ShoppingCart.UserId == userId)
                .FirstOrDefaultAsync();

            if (giftToRemove == null)
            {
                return false; // Return false if no matching gift is found
            }

            _context.GiftShoppingCartModel.Remove(giftToRemove);
            var res = await _context.SaveChangesAsync();

            return res > 0;
        }

        //remove one gift from cart
        public async Task<bool> RemoveOne(int giftId, int userId)
        {
            var giftToRemove = await _context.GiftShoppingCartModel
            .Where(g => g.DonationId == giftId && g.ShoppingCart.UserId == userId)
            .FirstOrDefaultAsync();

            if (giftToRemove == null)
            {
                return false; // Return false if no matching gift is found
            }
            var quantity = giftToRemove.Quantity;
            if (quantity > 1)
            {
                giftToRemove.Quantity -= 1;
            }
            else
            {
                _context.GiftShoppingCartModel.Remove(giftToRemove);
            }

            var res = await _context.SaveChangesAsync();
            return res > 0;
        }
        //add one to cart
        public async Task<bool> AddOneToCart(int giftId, int userId)
        {
            var giftToAdd = await _context.GiftShoppingCartModel
            .Where(g => g.DonationId == giftId && g.ShoppingCart.UserId == userId)
            .FirstOrDefaultAsync();

            if (giftToAdd == null)
            {
                return false; // Return false if no matching gift is found
            }

            giftToAdd.Quantity += 1;

            var res = await _context.SaveChangesAsync();
            return res > 0;

        }

        public async Task<bool> UpdateStatusCart(int cartId,int quantity)
        {
            var cart = await _context.ShoppingCartModel.FindAsync(cartId);
            if (cart == null)
            {
                return false; // Return false if no matching cart is found
            }
            cart.Status = CartStatus.Purchased; // Update status to completed using the enum value
            //var newPurchase = new PurchasesModel
            //{
            //    UserId = cart.UserId,
            //    DonationId = cart.GiftShoppingCart.First().DonationId, // Assuming at least one item exists
            //    ShoppingCartId = cartId,
            //    PurchaseDate = DateTime.UtcNow
            //};

            //for (int i = 0; i < quantity; i++)
            //{
            //    await _context.PurchasesModel.AddAsync(newPurchase);
            //}

            var res = await _context.SaveChangesAsync();
            return res > 0;
        }
    }
}
