using project.Manage.Models;

namespace project.Models.Customer
{
    public class GiftShoppingCartModel
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int DonationId { get; set; }
        public DonationsModel? Donations { get; set; }
        public int ShoppingCartId { get; set; }
        public ShoppingCartModel? ShoppingCart { get; set; }
    }
}
