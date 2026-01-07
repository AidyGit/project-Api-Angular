using Microsoft.AspNetCore.Mvc;
using project.Manage.Dtos;

namespace project.Manage.Interfaces
{
    public interface IRandomRepository
    {
        Task<List<RandomDto>> GetWinnerToDonation();
    }
}