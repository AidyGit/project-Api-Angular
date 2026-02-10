using Microsoft.EntityFrameworkCore;
using project.Manage.Models;

namespace project.Customer.Dtos
{
    public class GiftDto
    {
        public class GiftDetailDto
        {
            public int Id { get; set; }
            public string? Name { get; set; }
            public string? Description { get; set; }
            public int Category { get; set; }
            public int PriceTiket { get; set; }
            public string? ImageUrl { get; set; }
            public int Quantity { get; set; }
            public int ShoppingCartId { get; set; }
        }
        public class DetailInCartDto
        {
            public string Name { get; set; } = string.Empty;
            public string? UserName { get;set; }
            public int PriceTiket { get; set; }
            public string? ImageUrl { get; set; }
        }

    }
}
