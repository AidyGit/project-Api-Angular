using Microsoft.EntityFrameworkCore;
using project.Data;
using project.Manage.Dtos;
using project.Manage.Interfaces;
using project.Manage.Models;

namespace project.Manage.Repository
{
    public class PurchasesRepository:IPurchasesRepository
    {
        private readonly ApplicationDbContext _context;

        public PurchasesRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        //get purchases by donation id
        public async Task<IEnumerable<PurchasesModel>> GetPuchasesByDonation(int donationId)
        {
            var Purchases = await _context.PurchasesModel.Where(x => x.DonationId == donationId).ToListAsync();
            return Purchases;
        }

        //get all purchases
        public async Task<IEnumerable<PurchasesModel>> GetPuchases()
        {
            //var Purchases = await _context.PurchasesModel.Include(d => d.Donations).ToListAsync();

            var Purchases = await _context.PurchasesModel.ToListAsync();
            return Purchases;
        }
    }
}
