using project.Manage.Dtos;
using project.Manage.Interfaces;

namespace project.Manage.Services
{
    public class DonorService:IDonorsService
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
    }
}
