using project.Manage.Dtos;

namespace project.Manage.Interfaces
{
    public interface IPurchasesService
    {
        Task<IEnumerable<GetDonationWithPurchase>> GetPuchasesByDonation(int donationId);
        Task<IEnumerable<GetDonationWithPurchase>> GetPuchases();
    }
}
