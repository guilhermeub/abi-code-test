using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales;

/// <summary>
/// Response model for GetSale operation
/// </summary>
public class ListSalesResult
{
    /// <summary>
    /// The list of sales
    /// </summary>
    public List<Sale>? SalesList { get; set; }
}
