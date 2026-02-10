using project.Manage.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace project.Models.Customer
{
    public class GiftShoppingCartModel
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int DonationId { get; set; }
        [ForeignKey("DonationId")]

        public DonationsModel? Donations { get; set; }
        public int ShoppingCartId { get; set; }
        [ForeignKey("ShoppingCartId")] 
        public ShoppingCartModel? ShoppingCart { get; set; }
    }
}
