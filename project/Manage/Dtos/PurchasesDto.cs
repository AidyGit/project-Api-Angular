namespace project.Manage.Dtos
{
    public class PurchasesDto
    {
        public DateTime PurchaseDate { get; set; }
        public int UserId { get; set; }
        public int DonationId { get; set; }
        public int ShoppingCartId { get; set; }
        public string DonationName { get; set; } // חובה לצפייה בשם המתנה
        public int Price { get; set; }       // חובה למיון המתנה היקרה
        public string UserName { get; set; }     // חובה לצפייה בפרטי רוכשים
        public string UserEmail { get; set; }    // חובה לצפייה בפרטי רוכשים
    }
}
