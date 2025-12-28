namespace project.Manage.Models
{
    public enum Category
    {
        Electronics,
        Vacation,
        Food,
        Other
    }

    public class DonationsModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public int PriceTiket { get; set; }
        public int DonorsId { get; set; }
        public DonorsModel Donors { get; set; }
        public string? ImageUrl { get; set; }

        public ICollection<PurchasesModel> PurchasesModel = new List<PurchasesModel>();


    }
}
