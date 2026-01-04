using Microsoft.IdentityModel.Tokens;
using project.Manage.Dtos;
using project.Manage.Interfaces;
using project.Manage.Models;
using project.Manage.Repository;
using System.Drawing;

namespace project.Manage.Services
{
    public class DonationService:IDonationService
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
            var donation =await _donationRepository.GetDonationById(id);
            if (donation == null)
            {
                return null;
            }
            if (!donationDto.Name.IsNullOrEmpty())
                donation.Name = donationDto.Name;
            else
                donation.Name = donation.Name;
            if (!donationDto.ImageUrl.IsNullOrEmpty())
                donation.ImageUrl = donationDto.ImageUrl;
            else
                donation.ImageUrl = donation.ImageUrl;
            if (!donationDto.Description.IsNullOrEmpty())
                donation.Description = donationDto.Description;
            else
                donation.Description = donation.Description;
            if(donationDto.PriceTiket != 0)
                donation.PriceTiket = donationDto.PriceTiket;
            else
                donation.PriceTiket = donation.PriceTiket;
            if(donationDto.CategoryId != 0)
                donation.CategoryId = donationDto.CategoryId;
            else
                donation.CategoryId = donation.CategoryId;
            if(donationDto.DonorsId != 0)
                donation.DonorsId = donationDto.DonorsId;
            else
                donation.DonorsId = donation.DonorsId;
            var updatedDonation = new CreateDonationDto
            {
                Name = donation.Name,
                ImageUrl = donation.ImageUrl,
                Description = donation.Description,
                CategoryId = donation.CategoryId,
                DonorsId = donation.DonorsId,
                PriceTiket = donation.PriceTiket

            };
            await _donationRepository.UpdateDonation(donation);
            return updatedDonation;

        }
    }
}
