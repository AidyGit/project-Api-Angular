using project.Manage.Models;

namespace project.Models.Customer
{
    public class UserModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; } = null;
        public string? Phone { get; set; }
        public string Role { get; set; } = "user";
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<PurchasesModel> Purchases { get; set; } = new List<PurchasesModel>();
        public ICollection<ShoppingCartModel> ShoppingCarts { get; set; } = new List<ShoppingCartModel>();

    }
}
