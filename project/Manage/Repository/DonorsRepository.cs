using Microsoft.EntityFrameworkCore;
using project.Data;
using project.Manage.Dtos;
using project.Manage.Interfaces;

namespace project.Manage.Repository
{
    public class DonorsRepository: IDonorsRepository
    {
        private readonly ApplicationDbContext _context;

        public DonorsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        //get donors
        public async Task<IEnumerable<DonorsDto>> GetDonors()
        {
            return await _context.DonorsModel
                //.Include(x=>x.Donations)
                .Select(d => new DonorsDto
                {
                    Name = d.Name,
                    Email = d.Email,
                    Phone = d.Phone,
                    Donations = d.Donations.Select(don => new DonationDto
                    {
                        DescriptionDonation = don.Description,
                        NameDonation = don.Name,
                        PriceTiketDonation = don.PriceTiket
                    }).ToList()
                }).ToListAsync();
        }
    }
}
