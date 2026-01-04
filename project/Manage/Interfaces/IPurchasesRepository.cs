using project.Manage.Dtos;
using project.Manage.Models;

namespace project.Manage.Interfaces
{
    public interface IPurchasesRepository
    {
        Task<IEnumerable<PurchasesModel>> GetPuchasesByDonation(int donationId);
        Task<IEnumerable<PurchasesModel>> GetPuchases();

    }
}
