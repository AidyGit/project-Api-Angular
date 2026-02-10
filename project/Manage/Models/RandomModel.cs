namespace project.Manage.Models
{
    public class RandomModel
    {
        public int Id { get; set; }
        public int DonationId { get; set; }

        public DonationsModel? Donation { get; set; }

        //public ICollection<DonationsModel> donationsModels = new List<DonationsModel>();
        public int WinningPurchaseId { get; set; }
        public PurchasesModel WinningPurchase { get; set; } = null!;
        public  DateTime RaffleDate { get; set; }
        //static
    }

}

