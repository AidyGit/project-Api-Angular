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

        public async Task<IEnumerable<PurchasesDto>> GetPuchasesByDonation(int donationId)
        {
            var purchases = await _purchasesRepository.GetPuchasesByDonation(donationId);
            var donationWithPurchaseList = purchases.Select(MapToPurchaseDto).ToList();
            return donationWithPurchaseList;
        }

        public async Task<IEnumerable<PurchasesDto>> GetPuchases()
        {
            var purchases = await _purchasesRepository.GetPuchases();
            var purchasesMap = purchases.Select(MapToPurchaseDto).ToList(); // Fixed: Use Select to map each item in the collection
            return purchasesMap;
        }

        private static PurchasesDto MapToPurchaseDto(PurchasesModel Purchases)
        {
            return new PurchasesDto
            {
                UserId = Purchases.UserId,
                PurchaseDate = Purchases.PurchaseDate,
                DonationId = Purchases.DonationId,
                ShoppingCartId = Purchases.ShoppingCartId ?? 0 
            };
        }

        public async Task<IEnumerable<PurchasesDto>> GetPurchasesBySort(string sortBy)
        {
            var purchases = await _purchasesRepository.GetPuchasesWithDonation();
            return sortBy switch
            {
                "price_high" => purchases.OrderByDescending(p => p.Donations.PriceTiket).Select(MapToPurchaseDto),
                //"price_low" => purchases.OrderBy(p => p.Donations.PriceTiket).Select(MapToPurchaseDto),
                "most_purchased" => purchases.GroupBy(p => p.DonationId)
                                              .OrderByDescending(g => g.Count())
                                              .SelectMany(g => g)
                                              .Select(MapToPurchaseDto),
                _ => purchases.OrderByDescending(p => p.PurchaseDate).Select(MapToPurchaseDto)
            };
        }

        public async Task<byte[]> GetRevenueExcelFileAsync()
        {
            // 1. שליפת הנתונים מהריפוזיטורי
            var allPurchases = await _purchasesRepository.GetPuchasesWithDonation();

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
