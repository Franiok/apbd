using Kolos2.DTOs;

namespace Kolos2.Services;

public interface IDbService
{
    Task<PurchaseDto> GetOrderById(int purchaseId);
}