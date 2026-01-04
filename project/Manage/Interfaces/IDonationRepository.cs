using project.Manage.Dtos;
using project.Manage.Models;

namespace project.Manage.Interfaces
{
    public interface IDonationRepository
    {
        Task<IEnumerable<GetDonationDto>> GetDonations();
        Task<bool> AddDonation(CreateDonationDto donationDto);
        Task<bool> DeleteDonation(DonationsModel donation);
        Task<DonationsModel> UpdateDonation(DonationsModel donationDto);
        Task<DonationsModel> GetDonationById(int id);

    }
}
