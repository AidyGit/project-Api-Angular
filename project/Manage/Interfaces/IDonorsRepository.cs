using project.Manage.Dtos;
using project.Manage.Models;
using static project.Manage.Controller.DonorsController;

namespace project.Manage.Interfaces
{
    public interface IDonorsRepository
    {
        Task<IEnumerable<DonorsDto>> GetDonors();
        Task<DonorsModel> GetDonorsById(int id);

        Task<DonorsModel> AddDonor(DonorsDto donorDto);
        Task<bool> DeleteDonor(DonorsModel donor);
        Task<DonorsModel> UpdateDonor(DonorsModel donor);
        Task<IEnumerable<DonorsDto>> FilterDonors(DonorFilterParams donorFilterParams);

    }
}
