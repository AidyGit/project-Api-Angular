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

        //public async Task<bool> UpdateStatusCart(int cartId, int quantity)
        //{
        //    var cart = await _context.ShoppingCartModel
        //        .Include(c => c.GiftShoppingCart)

        //                .ThenInclude(g => g.Donations)

        //        .FirstOrDefaultAsync(c => c.Id == cartId);

        //    Console.WriteLine(cart != null ? "Cart found" : "Cart not found");
        //    Console.WriteLine(cart.GiftShoppingCart.Count); // צריך להיות > 0

        //    if (cart == null)
        //    {
        //        return false; // Return false if no matching cart is found
        //    }

        //    // בדיקה אם צריך לעדכן סטטוס
        //    //bool statusChanged = false;
        //    //if (cart.Status != CartStatus.Purchased)
        //    //{
        //    //    cart.Status = CartStatus.Purchased;
        //    //    statusChanged = true;
        //    //}
        //    _context.Entry(cart).Property(c => c.Status).IsModified = true;
        //    var cartItems = cart.GiftShoppingCart.ToList();
        //    //bool purchasesAdded = false;


        //    cart.Status = CartStatus.Purchased; // Update status to completed using the enum value


        //    foreach (var item in cartItems)
        //    {
        //        if (item.Donations == null)
        //            continue;



        //        for (int i = 0; i < item.Quantity; i++)
        //        {
        //            // הוספה ישירה ל-DbSet של הרכישות
        //            _context.PurchasesModel.Add(new PurchasesModel
        //            {
        //                UserId = cart.UserId,
        //                DonationId = item.DonationId,
        //                PurchaseDate = DateTime.UtcNow
        //            });
        //        }
        //        //purchasesAdded = true;

        //        _context.GiftShoppingCartModel.Remove(item);

        //    }
        //    //foreach (var item in cart.GiftShoppingCart.ToList())
        //    //{
        //    //    for (int i = 0; i < item.Quantity; i++)
        //    //    {
        //    //        _context.Add(new PurchasesModel
        //    //        {
        //    //            UserId = cart.UserId,
        //    //            DonationId = item.DonationId,
        //    //            PurchaseDate = DateTime.UtcNow
        //    //        });
        //    //    }

        //    //_context.GiftShoppingCartModel.Remove(item);

        //    var res = await _context.SaveChangesAsync();
        //    return res > 0;
        //}

        //public async Task<bool> UpdateStatusCart(int cartId, int quantity)
        //{
        //    // 1. טען את הסל
        //    var cart = await _context.ShoppingCartModel
        //        .FirstOrDefaultAsync(c => c.Id == cartId);

        //    if (cart == null) return false;

        //    bool statusChanged = false;
        //bool purchasesAdded = false;

        //// 2. עדכן סטטוס
        //if (cart.Status != CartStatus.Purchased)
        //{
        //    cart.Status = CartStatus.Purchased;
        //    statusChanged = true;
        //}

        //// 3. טעינת פריטים - ודא שה-Include עובד
        //var cartItems = await _context.GiftShoppingCartModel
        //    .Where(g => g.ShoppingCartId == cartId)
        //    .Include(g => g.Donations)
        //    .ToListAsync();

        //// בדיקה: האם בכלל נמצאו פריטים?
        //if (!cartItems.Any())
        //{
        //    // אם הגעת לכאן, סימן שאין פריטים בסל עם ה-ID הזה
        //        await _context.SaveChangesAsync();
        //        return statusChanged;
        //    }

        //    foreach (var item in cartItems)
        //    {
        //        // בדיקה: האם ה-Donation חסר?
        //        if (item.Donations == null) continue;

        //        // שימוש ב-item.Quantity או בפרמטר quantity מהפונקציה? 
        //        // כרגע זה משתמש במה שיש בסל:
        //        for (int i = 0; i < item.Quantity; i++)
        //        {
        //            _context.PurchasesModel.Add(new PurchasesModel
        //            {
        //                UserId = cart.UserId,
        //                DonationId = item.Donation,
        //                PurchaseDate = DateTime.UtcNow
        //            });
        //            purchasesAdded = true;
        //        }

        //        _context.GiftShoppingCartModel.Remove(item);
        //    }

        //    await _context.SaveChangesAsync();
        //    return statusChanged || purchasesAdded;
        //}

        public async Task<bool> UpdateStatusCart(int cartId, int quantity)
        {
            // טעינה ספציפית של העגלה עם הפריטים שלה
            var cart = await _context.ShoppingCartModel
                .Include(c => c.GiftShoppingCart)
                .ThenInclude(g => g.Donations)
                .FirstOrDefaultAsync(c => c.Id == cartId);

            if (cart == null) return false;

            // עדכון הסטטוס לעגלה הזו בלבד
            cart.Status = CartStatus.Purchased;

            var cartItems = cart.GiftShoppingCart.ToList();

            foreach (var item in cartItems)
            {
                if (item.DonationId <= 0) continue;
                //if (item.Donations == null) continue;

                // הוספת רשומות רכישה כמספר הכמות (Tickets)
                for (int i = 0; i < item.Quantity; i++)
                {
                    _context.PurchasesModel.Add(new PurchasesModel
                    {
                        UserId = cart.UserId,
                        DonationId = item.DonationId,
                        PurchaseDate = DateTime.UtcNow,
                        ShoppingCartId = item.ShoppingCartId
                    });
                }

                // מחיקת הפריט מהסל
                _context.GiftShoppingCartModel.Remove(item);
            }

            // ביצוע השמירה - מחזיר את מספר השורות שהשתנו (עדכון 1 + הוספות + מחיקות)
            try
            {
                var res = await _context.SaveChangesAsync();
                return res > 0;
            }
            catch (DbUpdateException ex)
            {
                throw;
            }
        }
    }

}
