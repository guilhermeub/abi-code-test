using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for Sale entity operations
/// </summary>
public interface ISaleRepository
{
    Task<Sale> GetSaleByIdAsync(Guid saleId, CancellationToken cancellationToken = default);
    Task<List<Sale>> GetSalesByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken = default);
    Task<Sale> CreateSaleAsync(Sale sale, CancellationToken cancellationToken = default);
    Task UpdateSaleAsync(Sale sale, CancellationToken cancellationToken = default);
    Task DeleteSaleAsync(Guid saleId, CancellationToken cancellationToken = default);
    Task<SaleItem> GetSaleItemByIdAsync(Guid saleId, Guid itemId, CancellationToken cancellationToken = default);
    Task CancelSaleItemAsync(Guid saleId, Guid itemId, CancellationToken cancellationToken = default);
}
