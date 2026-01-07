using project.Manage.Dtos;

namespace project.Manage.Interfaces
{
    public interface IRandomRepository
    {
        Task<RandomDto> GetWinnerToDonation();
    }
}