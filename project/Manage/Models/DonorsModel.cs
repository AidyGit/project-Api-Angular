using System.ComponentModel.DataAnnotations;

namespace project.Manage.Models
{
    public class DonorsModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public ICollection<DonationsModel> Donations { get; set; } = new List<DonationsModel>();
    }
}
