using project.Customer.Dtos;
using project.Customer.Interfaces;
using project.Customer.Repository;
using project.Models.Customer;
using System.Collections.Generic;

namespace project.Customer.Services
{
    public class GiftService : IGiftService
    {
        private readonly IGiftRepository _giftRepository;
        private readonly IUserRepository _userRepository;

        public GiftService(IGiftRepository giftRepository, IUserRepository userRepository)
        {
            _giftRepository = giftRepository;
            _userRepository = userRepository;
        }

        public async Task<bool> AddGiftToCart(int giftId, string userName, int quantity)
        {
            // 1. בדיקת קיום המשתמש
            var findUser = await _userRepository.GetUserByUserName(userName);
            if (userName == null || findUser == null)
            {
                throw new ArgumentException("Invalid giftId or userId not exist");
            }

            // 2. בדיקת קיום המתנה
            var findGift = await _giftRepository.FindGiftById(giftId);
            if (findGift == null)
                throw new ArgumentException("this gift not fount");

            var activeCart = await _giftRepository.GetOrCreateActiveCart(findUser.Id);
            //var activeCart = findUser.ShoppingCarts.FirstOrDefault(c=>c.Status==0);
            // 3. מציאת סל קניות קיים או יצירת חדש אם לא קיים
            if (activeCart == null)
            {
                activeCart = new ShoppingCartModel()
                {
                    UserId = findUser.Id,
                };
                // אם הסל חדש, צריך להוסיף אותו למסד הנתונים

                await _giftRepository.CreateShoppingCart(activeCart);
            }

            // 4. בדיקה האם המתנה כבר נמצאת בסל (כדי לעדכן כמות במקום להוסיף שורה)
            var existingItem = activeCart.GiftShoppingCart.FirstOrDefault(g => g.DonationId == giftId);

            if (existingItem != null)
            {

                existingItem.Quantity += quantity;
            }
            else
            {
                var cart = new GiftShoppingCartModel()
                {
                    DonationId = giftId,
                    Quantity = quantity,
                    ShoppingCartId = activeCart.Id

                };
                activeCart.GiftShoppingCart.Add(cart);
            }

            return await _giftRepository.SaveChangesInShoppingCard();
        }
        //get gifts
        public async Task<IEnumerable<GiftDto.GiftDetailDto>> GetMyCart(string userName)
        {
            var findUser = await _userRepository.GetUserByUserName(userName);
            if (userName == null || findUser == null)
            {
                throw new ArgumentException("Invalid giftId or userId not exist");
            }


            return await _giftRepository.GetMyCart(findUser.Id);
        }

        //remove from cart
        public async Task<bool> RemoveGiftFromCart(int giftId, string userName)
        {
            //אחרי שיהיה לנו תוקן נשנה כאן!!!!!
            var findUser = await _userRepository.GetUserByUserName(userName);
            if (userName == null || findUser == null)
            {
                throw new ArgumentException("Invalid giftId or userId not exist");
            }

            return await _giftRepository.RemoveGiftFromCart(giftId, findUser.Id);
        }
        public async Task<bool> RemoveOne(int giftId, string userName)
        {
            //אחרי שיהיה לנו תוקן נשנה כאן!!!!!
            var findUser = await _userRepository.GetUserByUserName(userName);
            if (userName == null || findUser == null)
            {
                throw new ArgumentException("Invalid giftId or userId not exist");
            }

            return await _giftRepository.RemoveOne(giftId, findUser.Id);
        }
        public async Task<bool> AddOneToCart(int giftId, string userName)
        {
            var findUser = await _userRepository.GetUserByUserName(userName);
            if (userName == null || findUser == null)
            {
                throw new ArgumentException("Invalid giftId or userId not exist");
            }

            return await _giftRepository.AddOneToCart(giftId, findUser.Id);
        }

        //public async Task<bool> UpdateCart(int giftId, string userName, int quantityChange)
        //{
        //    // 1. אימות משתמש (חילוץ לוגיקה חוזרת)
        //    var user = await _userRepository.GetUserByUserName(userName);
        //    if (user == null) throw new ArgumentException("User not found");

        //    // 2. מציאת סל או יצירתו
        //    var activeCart = user.ShoppingCarts.FirstOrDefault();
        //    if (activeCart == null)
        //    {
        //        activeCart = new ShoppingCartModel { UserId = user.Id };
        //        await _giftRepository.CreateShoppingCart(activeCart);
        //    }

        //    // 3. עדכון פריט קיים או הוספת חדש
        //    var existingItem = activeCart.GiftShoppingCart?.FirstOrDefault(g => g.DonationId == giftId);

        //    if (existingItem != null)
        //    {
        //        existingItem.Quantity += quantityChange;
        //        // אופציונלי: אם הכמות יורדת ל-0 או פחות, ניתן למחוק את השורה
        //        if (existingItem.Quantity <= 0 && activeCart.Status == 0)
        //            return await _giftRepository.RemoveGiftFromCart(giftId, user.Id);
        //    }
        //    else if (quantityChange > 0)
        //    {
        //        var newItem = new GiftShoppingCartModel
        //        {
        //            DonationId = giftId,
        //            Quantity = quantityChange,
        //            ShoppingCartId = activeCart.Id
        //        };
        //        activeCart.GiftShoppingCart.Add(newItem);
        //    }

        //    return await _giftRepository.SaveChangesInShoppingCard();
        //}

        public async Task<bool> UpdateStatusCart(int cartId)
        {
            return await _giftRepository.UpdateStatusCart(cartId);
        }
    }
}
