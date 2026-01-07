using project.Models.Customer;
using System.ComponentModel.DataAnnotations.Schema;

namespace project.Manage.Models
{
    public class PurchasesModel
    {
        public int Id { get; set; }
        public int DonationId { get; set; }
        public DonationsModel Donations { get; set; }
        public DateTime PurchaseDate { get; set; }

        public int UserId { get; set; }
        public UserModel User { get; set; }
        // אופציונלי – אם רוצים לדעת מאיזה סל הרכישה
        public int? ShoppingCartId { get; set; }
        public ShoppingCartModel ShoppingCart { get; set; }

    }
}
