namespace project.Manage.Dtos
{
    public class DonorsDto
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }

        public List<DonationDto> Donations { get; set; } = new List<DonationDto>();
    }
    public class DonationDto
    {
        public int PriceTiketDonation { get; set; }
        public string NameDonation { get; set; }
        public string DescriptionDonation { get; set; }
    }
}