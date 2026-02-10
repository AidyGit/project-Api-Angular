using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using project.Manage.Dtos;
using project.Manage.Interfaces;
using project.Manage.Models;
using static project.Manage.Controller.DonorsController;

namespace project.Manage.Services
{
    public class DonorService : IDonorsService
    {
        private readonly IDonorsRepository _donorsRepository;
        public DonorService(IDonorsRepository donorsRepository)
        {
            _donorsRepository = donorsRepository;
        }
        public async Task<IEnumerable<DonorsDto>> GetDonors()
        {
            var donors = await _donorsRepository.GetDonors();
            return donors.Select(d => MapToDonorsDto(d)).ToList();
        }
        public async Task<DonorsDto> AddDonor(DonorsDto donorsDto)
        {
            var donor =  await _donorsRepository.AddDonor(donorsDto);
            return new DonorsDto
            {
                Name = donor.Name,
                Email = donor.Email,
                Phone = donor.Phone
            };

        }
        //delete donor
        public async Task<bool> DeleteDonor(int id)
        {
            var donor = await _donorsRepository.GetDonorsById(id);
            if (donor == null)
            {
                return false;
            }
            return await _donorsRepository.DeleteDonor(donor);
        }
        // Update donor method with fixed IsNullOrEmpty usage
        public async Task<DonorsUpdateDto> UpdateDonor(int id, DonorsUpdateDto donorToUp)
        {
            var donor = await _donorsRepository.GetDonorsById(id);
            if (donor == null)
            {
                return null;
            }

            if (!string.IsNullOrEmpty(donorToUp.Name))
                donor.Name = donorToUp.Name;
            else
                donor.Name = donor.Name;

            if (!string.IsNullOrEmpty(donorToUp.Email))
                donor.Email = donorToUp.Email;
            else
                donor.Email = donor.Email;

            if (!string.IsNullOrEmpty(donorToUp.Phone))
                donor.Phone = donorToUp.Phone;
            else
                donor.Phone = donor.Phone;

            var updatedDonor = new DonorsUpdateDto
            {
                Name = donor.Name,
                Email = donor.Email,
                Phone = donor.Phone
            };

            await _donorsRepository.UpdateDonor(donor);
            return updatedDonor;
        }
        //filter donors
        public async Task<IEnumerable<DonorsDto>> FilterDonors(DonorFilterParams donorFilterParams)
        {
            var donors = await _donorsRepository.FilterDonors(donorFilterParams);
            return donors.Select(d => MapToDonorsDto(d)).ToList();
        }

        private static DonorsDto MapToDonorsDto(DonorsModel donors)
        {
            return new DonorsDto
            {
                Id = donors.Id,
                Email = donors.Email,
                Name = donors.Name,
                Phone = donors.Phone,
                Donations = donors.Donations.Select(don => new DonationDto
                {
                    DescriptionDonation = don.Description,
                    NameDonation = don.Name,
                    PriceTiketDonation = don.PriceTiket

                }).ToList()

            };
        }

    }
}
