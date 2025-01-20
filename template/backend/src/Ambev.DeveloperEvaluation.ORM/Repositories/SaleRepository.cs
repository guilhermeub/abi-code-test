using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of ISaleRepository using Entity Framework Core
/// </summary>
public class SaleRepository : ISaleRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of SaleRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public SaleRepository(DefaultContext context)
    {
        _context = context;
    }

    public async Task<Sale> GetSaleByIdAsync(Guid saleId, CancellationToken token = default)
    {
        return await _context.Sales
            .Include(s => s.Items)
            .FirstOrDefaultAsync(s => s.Id == saleId, cancellationToken: token);
    }

    public async Task<List<Sale>> GetSalesByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken = default)
    {
        return await _context.Sales
            .Include(s => s.Items)
            .Where(s => s.Customer.Id == customerId)
            .ToListAsync();
    }

    public async Task<Sale> CreateSaleAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        await _context.Sales.AddAsync(sale);
        await _context.SaveChangesAsync();
        return sale;
    }

    public async Task UpdateSaleAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        _context.Sales.Update(sale);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteSaleAsync(Guid saleId, CancellationToken token = default)
    {
        var sale = await GetSaleByIdAsync(saleId, token);
        if (sale != null)
        {
            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<SaleItem> GetSaleItemByIdAsync(Guid saleId, Guid itemId, CancellationToken cancellationToken = default)
    {
        return await _context.SaleItems
            .FirstOrDefaultAsync(si => si.Id == itemId && si.SaleId == saleId, cancellationToken: cancellationToken);
    }

    public async Task CancelSaleItemAsync(Guid saleId, Guid itemId, CancellationToken token = default)
    {
        var sale = await GetSaleByIdAsync(saleId, token);
        if (sale == null) return;

        var item = sale.Items.FirstOrDefault(p => p.Id == itemId);
        if (item != null)
        {
            item.IsCancelled = true;
            await _context.SaveChangesAsync();
        }
    }
}
