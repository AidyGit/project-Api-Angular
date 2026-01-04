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
            return await _donorsRepository.GetDonors();
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
        //update donor
        public async Task<DonorsUpdateDto> UpdateDonor(int id, DonorsUpdateDto donorToUp)
        {
            var donor = await _donorsRepository.GetDonorsById(id);
            if (donor == null)
            {
                return null;
            }
            if (!donorToUp.Name.IsNullOrEmpty())
                donor.Name = donorToUp.Name;
            else
                donor.Name = donor.Name;
            if (!donorToUp.Email.IsNullOrEmpty())
                donor.Email = donorToUp.Email;
            else
                donor.Email = donor.Email;
            if (!donorToUp.Phone.IsNullOrEmpty())
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
            return await _donorsRepository.FilterDonors(donorFilterParams);
        }
    }
}
