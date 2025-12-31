using project.Manage.Models;

namespace project.Customer.Dtos
{
    public class GiftDto
    {
        public class GiftDetailDto
        {
            public string? Name { get; set; }
            public string? Description { get; set; }
            public int Category { get; set; }
            public int PriceTiket { get; set; }
            public string? ImageUrl { get; set; }
        }
        public class DetailInCartDto
        {
            public string Name { get; set; } = string.Empty;
            public string? UserNmae { get;set; }
            public int PriceTiket { get; set; }
            public string? ImageUrl { get; set; }

        }

    }
}
