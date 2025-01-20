using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

/// <summary>
/// Response model for GetSale operation
/// </summary>
public class GetSaleResult
{
    /// <summary>
    /// The unique identifier of the sale
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The name or description of the sale
    /// </summary>
    public string SaleName { get; set; } = string.Empty;

    /// <summary>
    /// The date when the sale was made
    /// </summary>
    public DateTime SaleDate { get; set; }

    /// <summary>
    /// The name of the customer who made the purchase
    /// </summary>
    public string Customer { get; set; } = string.Empty;

    /// <summary>
    /// The total amount of the sale
    /// </summary>
    public decimal TotalSaleAmount { get; set; }

    /// <summary>
    /// The branch where the sale was made
    /// </summary>
    public string Branch { get; set; } = string.Empty;

    /// <summary>
    /// Indicates whether the sale has been canceled
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// The list of items included in the sale
    /// </summary>
    public List<GetSaleItemResult> Items { get; set; } = new List<GetSaleItemResult>();
}

/// <summary>
/// Response model for individual sale items
/// </summary>
public class GetSaleItemResult
{
    /// <summary>
    /// The unique identifier of the sale item
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The product name of the sale item
    /// </summary>
    public string Product { get; set; } = string.Empty;

    /// <summary>
    /// The quantity of the product sold
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// The unit price of the product
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// The discount applied to the product
    /// </summary>
    public decimal Discount { get; set; }

    /// <summary>
    /// The total amount for the sale item
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Indicates whether the sale item has been canceled
    /// </summary>
    public bool IsCancelled { get; set; }
}
