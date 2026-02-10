namespace project.Manage.Dtos
{
    public class PurchasesDto
    {
        public DateTime PurchaseDate { get; set; }
        public int UserId { get; set; }
        public int DonationId { get; set; }
        public int ShoppingCartId { get; set; }
    }
}
