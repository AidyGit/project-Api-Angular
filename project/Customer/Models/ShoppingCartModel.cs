using project.Manage.Models;

namespace project.Models.Customer
{
    public enum CartStatus
    {
        Draft = 0,
        Purchased = 1
    }

    public class ShoppingCartModel
    {
        public int Id { get; set; }
        public CartStatus Status { get; set; } = CartStatus.Draft;

        public ICollection<GiftShoppingCartModel> GiftShoppingCart { get; set; } = new List<GiftShoppingCartModel>();
        public int UserId { get; set; }
        public UserModel? User { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
