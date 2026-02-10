using Microsoft.AspNetCore.Mvc;
using project.Manage.Dtos;
using project.Manage.Models;

namespace project.Manage.Interfaces
{
    public interface IPurchasesService
    {
        Task<IEnumerable<PurchasesDto>> GetPuchasesByDonation(int donationId);
        Task<IEnumerable<PurchasesDto>> GetPuchases();
        Task<IEnumerable<PurchasesDto>> GetPurchasesBySort(string sortBy);
        Task<byte[]> GetRevenueExcelFileAsync();
    }
}
