

using Microsoft.EntityFrameworkCore;
using project.Manage.Models;
using project.Models.Customer;

namespace project.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<GiftShoppingCartModel> GiftShoppingCartModel { get; set; }
        public DbSet<ShoppingCartModel> ShoppingCartModel { get; set; }
        public DbSet<UserModel> UserModel { get; set; }
        public DbSet<DonationsModel> DonationsModel { get; set; }
        public DbSet<DonorsModel> DonorsModel { get; set; }
        public DbSet<PurchasesModel> PurchasesModel { get; set; }
        public DbSet<RandonModel> RandonModel { get; set; }


    }
}
