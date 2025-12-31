using project.Manage.Dtos;

namespace project.Manage.Interfaces
{
    public interface IDonorsService
    {
        Task<IEnumerable<DonorsDto>> GetDonors();
    }
}
