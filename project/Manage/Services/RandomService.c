using project.Customer.Dtos;
using project.Manage.Dtos;
using project.Manage.Interfaces;
using project.Manage.Models;
using project.Manage.Repository;
using project.Models.Customer;

namespace project.Manage.Services
{
    public class RandomService:IRandomService
    {
        private readonly IRandomRepository _randomRepository;
        public RandomService(IRandomRepository randomRepository)
        {
            _randomRepository = randomRepository;
        }
        public async Task<RandomDto> GetWinnerToDonation()
        {
            return await _randomRepository.GetWinnerToDonation();
        }
            
    }
}
