using project.Manage.Dtos;

namespace project.Manage.Interfaces
{
    public interface IDonorsRepository
    {
        Task<IEnumerable<DonorsDto>> GetDonors();
    }
}
