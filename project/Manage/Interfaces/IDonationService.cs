using Microsoft.AspNetCore.Mvc;
using project.Manage.Dtos;
using project.Manage.Models;
using static project.Manage.Controller.DonationController;

namespace project.Manage.Interfaces
{
    public interface IDonationService
    {
        Task<IEnumerable<GetDonationDto>> GetDonations();
        Task<bool> AddDonation(CreateDonationDto donationDto);
        Task<bool> DeleteDonation(int id);
        Task<CreateDonationDto> UpdateDonation(int id, CreateDonationDto donationDto);
        Task<IEnumerable<GetDonationWithPurchase>> SearchDonations(string? donationName,string? donorName,int? minPurchases);
        Task<int> GetIdByEmail(string email);
        Task<IEnumerable<CategoryModel>> GetAllCategories();
        Task<IEnumerable<GetDonationDto>> FilterDonation(DonationFilterParams DonorFilterParams);
    }
}
