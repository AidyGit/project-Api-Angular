using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using project.Manage.Dtos;
using project.Manage.Interfaces;
using project.Manage.Models;
using project.Manage.Repository;
using System.Drawing;
using static project.Manage.Controller.DonationController;

namespace project.Manage.Services
{
    public class DonationService : IDonationService
    {
        private readonly IDonationRepository _donationRepository;
        public DonationService(IDonationRepository donationRepository)
        {
            _donationRepository = donationRepository;
        }

        public async Task<IEnumerable<GetDonationDto>> GetDonations()
        {
            return await _donationRepository.GetDonations();
        }

        public async Task<bool> AddDonation(CreateDonationDto donationDto)
        {
            return await _donationRepository.AddDonation(donationDto);
        }
        public async Task<bool> DeleteDonation(int id)
        {
            var donation = await _donationRepository.GetDonationById(id);
            return await _donationRepository.DeleteDonation(donation);
        }

        public async Task<CreateDonationDto> UpdateDonation(int id, CreateDonationDto donationDto)
        {
            var donation = await _donationRepository.GetDonationById(id);
            if (donation == null)
            {
                return null;
            }
            if (!string.IsNullOrEmpty(donationDto.Name))
                donation.Name = donationDto.Name;
            else
                donation.Name = donation.Name;
            if (!string.IsNullOrEmpty(donationDto.ImageUrl))
                donation.ImageUrl = donationDto.ImageUrl;
            else
                donation.ImageUrl = donation.ImageUrl;
            if (!string.IsNullOrEmpty(donationDto.Description))
                donation.Description = donationDto.Description;
            else
                donation.Description = donation.Description;
            if (donationDto.PriceTiket != 0)
                donation.PriceTiket = donationDto.PriceTiket;
            else
                donation.PriceTiket = donation.PriceTiket;
            if (donationDto.CategoryId != 0)
                donation.CategoryId = donationDto.CategoryId;
            else
                donation.CategoryId = donation.CategoryId;
            if (donationDto.DonorsId != 0)
                donation.DonorsId = donationDto.DonorsId;
            else
                donation.DonorsId = donation.DonorsId;

            var updatedDonation = new CreateDonationDto
            {
                Name = donation.Name,
                ImageUrl = donation.ImageUrl,
                Description = donation.Description,
                CategoryId = (int)donation.CategoryId,
                DonorsId = donation.DonorsId,
                PriceTiket = donation.PriceTiket
            };

            await _donationRepository.UpdateDonation(donation);
            return updatedDonation;

        }
        public async Task<int> GetIdByEmail(string email)
        {
            var donors = await _donationRepository.getDonors();
            var donor = donors.FirstOrDefault(d => d.Email == email);
            if (donor == null)
            {
                return 0; // מחזירים 0 אם לא נמצא, כדי שהאנגולר ידע שאין תורם כזה
            }
            return donor.Id;
        }
        public async Task<IEnumerable<CategoryModel>> GetAllCategories()
        {
            return await _donationRepository.GetAllCategories();
        }

        public async Task<IEnumerable<GetDonationWithPurchase>> SearchDonations(
        string donationName, string donorName, int? minPurchases)
        {
            return await _donationRepository.SearchDonations(donationName, donorName, minPurchases);
        }

        public async Task<IEnumerable<GetDonationDto>> FilterDonation(DonationFilterParams DonorFilterParams) 
        { 
            return await _donationRepository.FilterDonation(DonorFilterParams); 
        }
        public static GetDonationDto MapToDonationDto(DonationsModel donation)
        {
            return new GetDonationDto
            {
                Id = donation.Id,
                Name = donation.Name,
                Description = donation.Description,
                PriceTiket = donation.PriceTiket,
                DonorsId = donation.DonorsId,
                ImageUrl = donation.ImageUrl,
                DonorName = donation.Donors?.Name ?? "לא ידוע",
                CategoryName = donation.Category?.Name ?? "ללא קטגוריה",
                WinnerName = "אין זוכה עדיין"
            };
        }
    }
}
