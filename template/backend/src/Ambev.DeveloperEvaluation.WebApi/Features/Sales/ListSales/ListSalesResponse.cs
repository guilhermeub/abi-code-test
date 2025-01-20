using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

/// <summary>
/// API response model for GetSale operation
/// </summary>
public class ListSalesResponse
{
    /// <summary>
    /// The list of sales
    /// </summary>
    public List<Sale> SalesList { get; set; }
}
