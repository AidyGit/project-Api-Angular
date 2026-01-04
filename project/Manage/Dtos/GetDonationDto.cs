namespace project.Manage.Dtos
{
    public class GetDonationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PriceTiket { get; set; }
        public string? ImageUrl { get; set; }
        public string categoryName { get; set; }
        public int DonorsId { get; set; }
    }
    //public class CategoryDto
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //    public string Description { get; set; }
    //}
    public class CreateDonationDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int PriceTiket { get; set; }
        public int CategoryId { get; set; }
        public int DonorsId { get; set; }
        public string? ImageUrl { get; set; }
    }
}
