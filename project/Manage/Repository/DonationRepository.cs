using Microsoft.EntityFrameworkCore;
using project.Data;
using project.Manage.Dtos;
using project.Manage.Interfaces;
using project.Manage.Models;

namespace project.Manage.Repository
{
    public class DonationRepository:IDonationRepository
    {
        private readonly ApplicationDbContext _context;

        public DonationRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<GetDonationDto>> GetDonations()
        {

            // Placeholder logic for getting donations from the database
            return await _context.DonationsModel.Select(d => new GetDonationDto
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description,
                PriceTiket = d.PriceTiket,
                categoryName = d.Category.Name,
                DonorsId = d.DonorsId,

            }).ToListAsync();
        }
        public async Task<bool> AddDonation(CreateDonationDto donationDto)
        {
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

        public async Task<DonationsModel> UpdateDonation(DonationsModel donationDto)
        {
            //ID חפוש במסד התונים את התרומה לפי ה
            var existing = await _context.DonationsModel.FindAsync(donationDto.Id);
            if (existing == null) return null ;

            //עדכון הערכים של התרומה הקיימת עם הערכים החדשים מהאובייקט donationDto
            _context.Entry(existing).CurrentValues.SetValues(donationDto);

            await _context.SaveChangesAsync();
            return existing;
        }
    }
}

