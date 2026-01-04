using project.Customer.Dtos;
using project.Manage.Dtos;
using project.Manage.Interfaces;
using project.Manage.Models;
using project.Manage.Repository;
using project.Models.Customer;

namespace project.Manage.Services
{
    public class PurchasesService:IPurchasesService
    {
        private readonly IPurchasesRepository _purchasesRepository;
        public PurchasesService(IPurchasesRepository purchasesRepository)
        {
            _purchasesRepository = purchasesRepository;
        }

        public async Task<IEnumerable<GetDonationWithPurchase>> GetPuchasesByDonation(int donationId)
        {
            var purchases = await _purchasesRepository.GetPuchasesByDonation(donationId);
            var donationWithPurchaseList = purchases.Select(MapToPurchaseDto).ToList();
            return donationWithPurchaseList;
        }

        public async Task<IEnumerable<GetDonationWithPurchase>> GetPuchases()
        {
            var purchases = await _purchasesRepository.GetPuchases();
            var donationWithPurchaseList = purchases.Select(MapToPurchaseDto).ToList();
            return donationWithPurchaseList;
        }

        private static GetDonationWithPurchase MapToPurchaseDto(PurchasesModel Purchases)
        {
            return new GetDonationWithPurchase
            {
                DonorsId = Purchases.DonationId,
                Description = Purchases.Donations.Description,
                CategoryId = Purchases.Donations.CategoryId,
                ImageUrl = Purchases.Donations.ImageUrl,
                Name = Purchases.Donations.Name,
                PriceTiket = Purchases.Donations.PriceTiket,
                Purchases = Purchases.Donations.PurchasesModel.Select(p => new PurchasesDto
                {
                    UserId = p.UserId,
                    PurchaseDate = p.PurchaseDate
                }).ToList()
            };
        }

    }
}
