namespace project.Manage.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<DonationsModel> Donations { get; set; } = new List<DonationsModel>();
    }
}
