using Microsoft.EntityFrameworkCore;
using project.Data;
using project.Manage.Dtos;
using project.Manage.Interfaces;
using project.Manage.Models;

namespace project.Manage.Repository
{
    public class DonationRepository : IDonationRepository
    {
        private readonly ApplicationDbContext _context;

        public DonationRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<GetDonationDto>> GetDonations()
        {

            // Placeholder logic for getting donations from the database
            return await _context.DonationsModel.Include(d => d.Category).Include(d=>d.Donors).Select(d => new GetDonationDto
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description,
                PriceTiket = d.PriceTiket,
                CategoryName = d.Category.Name,
                DonorsId = d.DonorsId,
                DonaorName = d.Donors.Name,
                ImageUrl = d.ImageUrl,
                WinnerName = _context.RandomModel
                .Where(r => r.DonationId == d.Id)
                .Select(r => r.WinningPurchase.User.Name) // שולפים את שם המשתמש מהרכישה הזוכה
                .FirstOrDefault() ?? "אין זוכה עדיין"

            }).ToListAsync();
        }
        public async Task<bool> AddDonation(CreateDonationDto donationDto)
        {
            var donorExists = await _context.DonorsModel.AnyAsync(d => d.Id == donationDto.DonorsId);
            if (!donorExists)
            {
                throw new Exception($"Donor with ID {donationDto.DonorsId} does not exist.");
            }

            var newDonation = new DonationsModel
            {
                Name = donationDto.Name,
                Description = donationDto.Description,
                PriceTiket = donationDto.PriceTiket,
                DonorsId = donationDto.DonorsId,
                ImageUrl = donationDto.ImageUrl,
                CategoryId = donationDto.CategoryId
            };
            _context.DonationsModel.Add(newDonation);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<DonationsModel> GetDonationById(int id)
        {
            var donation = await _context.DonationsModel.FirstOrDefaultAsync(d => d.Id == id);
            if (donation == null)
            {
                return null;
            }
            return donation;
        }
        public async Task<bool> DeleteDonation(DonationsModel donation)
        {
            if (donation == null)
            {
                return false;
            }
            _context.DonationsModel.Remove(donation);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<DonationsModel> UpdateDonation(DonationsModel donation)
        {
            //ID חפוש במסד התונים את התרומה לפי ה
            var existing = await _context.DonationsModel.FindAsync(donation.Id);

            if (existing == null) return null;

            //עדכון הערכים של התרומה הקיימת עם הערכים החדשים מהאובייקט donationDto
            existing.Name = donation.Name;
            existing.Description = donation.Description;
            existing.PriceTiket = donation.PriceTiket;
            existing.CategoryId = donation.CategoryId;
            existing.DonorsId = donation.DonorsId;
            existing.ImageUrl = donation.ImageUrl;

            await _context.SaveChangesAsync();

            return existing;
        }

        public async Task<IEnumerable<GetDonationWithPurchase>> SearchDonations(
            string donationName = null,
            string donorName = null,
            int? minPurchases = null)
        {
            var query = _context.DonationsModel
                .Include(d => d.PurchasesModel)
                .Include(d => d.Donors)
                .AsQueryable();

            if (!string.IsNullOrEmpty(donationName))
                query = query.Where(d => d.Name.Contains(donationName));

            if (!string.IsNullOrEmpty(donorName))
                query = query.Where(d => d.Donors.Name.Contains(donorName));

            if (minPurchases.HasValue)
                query = query.Where(d => d.PurchasesModel.Count >= minPurchases.Value);

            return await query.Select(d => new GetDonationWithPurchase
            {
                Name = d.Name,
                Description = d.Description,
                PriceTiket = d.PriceTiket,
                CategoryId = d.CategoryId,
                DonorsId = d.DonorsId,
                ImageUrl = d.ImageUrl,
                Purchases = d.PurchasesModel.Select(p => new PurchasesDto
                {
                    DonationId = p.DonationId,
                    UserId = p.UserId,
                    PurchaseDate = p.PurchaseDate
                }).ToList()
            }).ToListAsync();
        }

        public async Task<IEnumerable<DonorsModel>> getDonors()
        {
            return await _context.DonorsModel.ToListAsync();
        }
        public async Task<IEnumerable<CategoryModel>> GetAllCategories()
        {
            return await _context.CategoryModel.ToListAsync();
        }
    }
}

