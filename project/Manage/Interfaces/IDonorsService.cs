using project.Manage.Dtos;
using project.Manage.Models;
using static project.Manage.Controller.DonorsController;

namespace project.Manage.Interfaces
{
    public interface IDonorsService
    {
        Task<IEnumerable<DonorsDto>> GetDonors();
        Task<DonorsDto> AddDonor(DonorsDto donorDto);
        Task<bool> DeleteDonor(int id);
        //Task<bool> GetDonorsById(int id);
        Task<DonorsUpdateDto> UpdateDonor(int id, DonorsUpdateDto donorToUp);
        Task<IEnumerable<DonorsDto>> FilterDonors(DonorFilterParams donorFilterParams);

    }
}
