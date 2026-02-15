using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using project.Data;
using project.Manage.Dtos;
using project.Manage.Interfaces;
using project.Manage.Models;
using static project.Manage.Controller.DonorsController;

namespace project.Manage.Repository
{
    public class DonorsRepository : IDonorsRepository
    {
        private readonly ApplicationDbContext _context;

        public DonorsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //get donors
        public async Task<IEnumerable<DonorsModel>> GetDonors()
        {
            return await _context.DonorsModel.Include(d=>d.Donations).ToListAsync();
        }

        public async Task<DonorsModel> GetDonorsById(int id)
        {
            var donor =  await _context.DonorsModel.FirstOrDefaultAsync(d => d.Id == id);
            if(donor == null)
            {
                return null;
            }
            return donor;
        }

        //add donor
        public async Task<DonorsModel> AddDonor(DonorsDto donorsDto)
        {
            var newDonor = new DonorsModel
            {
                Name = donorsDto.Name,
                Email = donorsDto?.Email,
                Phone = donorsDto?.Phone,
                ImgUrl = donorsDto?.ImgUrl
            };
            _context.DonorsModel.Add(newDonor);
            await _context.SaveChangesAsync();
            return newDonor;
        }
        //delete donor
        public async Task<bool> DeleteDonor(DonorsModel donor)
        {
            _context.DonorsModel.Remove(donor);
            await _context.SaveChangesAsync();
            return true;
        }
        //update donor
        public async Task<DonorsModel> UpdateDonor(DonorsModel donor)
        {
            var existing = await _context.DonorsModel.FindAsync(donor.Id);
            if (existing == null) return null;

            _context.Entry(existing).CurrentValues.SetValues(donor);

            await _context.SaveChangesAsync();
            return existing;

        }
        //filter donors
        public async Task<IEnumerable<DonorsModel>> FilterDonors(DonorFilterParams donorFilterParams)
        {
            var query = _context.DonorsModel.Include(d=>d.Donations).AsQueryable();
            if (!string.IsNullOrEmpty(donorFilterParams.Name))
            {
                query = query.Where(d => d.Name.Contains(donorFilterParams.Name));
            }
            if (!string.IsNullOrEmpty(donorFilterParams.Email))
            {
                query = query.Where(d => d.Email.Contains(donorFilterParams.Email));
            }
            if (!string.IsNullOrEmpty(donorFilterParams.NameGift))
            {
                query = query.Where(d => d.Donations.Any(don => don.Name.Contains(donorFilterParams.NameGift)));
            }

            return await query.ToListAsync();
        }
    }
}
