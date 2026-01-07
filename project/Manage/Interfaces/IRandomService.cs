using project.Manage.Dtos;
using project.Manage.Dtos;
using project.Manage.Models;

namespace project.Manage.Interfaces
{
    public interface IRandomService
    {
        Task<List<RandomDto>> GetWinnerToDonation();
            }
}
