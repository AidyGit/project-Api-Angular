namespace project.Manage.Models
{

    public class DonationsModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public CategoryModel Category { get; set; }
        public int PriceTiket { get; set; }
        public int DonorsId { get; set; }
        public DonorsModel Donors { get; set; }
        public string? ImageUrl { get; set; }

        public ICollection<PurchasesModel> PurchasesModel { get; set; } = new List<PurchasesModel>();
    }
}
