using project.Customer.Dtos;
using project.Manage.Dtos;
using project.Manage.Function;
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

        public async Task<byte[]> GetRevenueExcelFileAsync()
        {
            // 1. שליפת הנתונים מהריפוזיטורי
            var allPurchases = await _purchasesRepository.GetPuchases();

            // 2. יצירת הדוח והתאמה ל-DTO שלך
            var reportData = allPurchases
                .GroupBy(p => p.DonationId)
                .Select(group => new TotalRevenueDto
                {
                    GiftName = group.First().Donations?.Name ?? "ללא שם",
                    TotalRevenue = group.Sum(p => p.Donations.PriceTiket) // כאן תוודאי ש-Price הוא השם במודל הרכישות
                }).ToList();

            // 3. שליחה להלפר שמתבסס על ה-DTO המעודכן
            return TotalRevenue.CreateRevenueExcel(reportData);
        }
    }
}
