using project.Manage.Dtos;
using project.Manage.Models;

namespace project.Manage.Interfaces
{
    public interface IDonationService
    {
        Task<IEnumerable<GetDonationDto>> GetDonations();
        Task<bool> AddDonation(CreateDonationDto donationDto);
        Task<bool> DeleteDonation(int id);
        Task<CreateDonationDto> UpdateDonation(int id, CreateDonationDto donationDto);
    }
}
