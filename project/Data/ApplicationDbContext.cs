

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //customer validation
            modelBuilder.Entity<UserModel>(e=>
            {
                e.Property(e => e.Name).IsRequired().HasMaxLength(50);
                e.Property(e => e.Email);
                e.Property(e => e.UserName).IsRequired().HasMaxLength(50);
                e.Property(e => e.Password).IsRequired();
                e.Property(e => e.Phone).HasMaxLength(10);

                e.HasMany(u => u.Purchases)
                 .WithOne(p => p.User)
                 .HasForeignKey(p => p.UserId)
                 .OnDelete(DeleteBehavior.Restrict);
                e.HasMany(u => u.ShoppingCarts)
                 .WithOne(s => s.User)
                 .HasForeignKey(s => s.UserId)
                 .OnDelete(DeleteBehavior.Restrict);
            });

            //mnage validation
            modelBuilder.Entity<DonationsModel>(e=> {
                e.Property(e => e.Name).IsRequired().HasMaxLength(100);
                e.Property(e => e.Category).IsRequired().HasMaxLength(20);
                //לבדוק אם נכון לעשות INT
                e.Property(e => e.PriceTiket).IsRequired();
            });

            modelBuilder.Entity<DonorsModel>(e=> {
                e.Property(e => e.Name).IsRequired().HasMaxLength(50);
                e.Property(e => e.Email).IsRequired();
                e.Property(e => e.Phone).HasMaxLength(10);
            } );

      
        }

    }
}
