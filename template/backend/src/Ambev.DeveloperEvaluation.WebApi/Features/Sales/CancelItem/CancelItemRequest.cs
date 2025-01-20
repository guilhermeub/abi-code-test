namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelItem;

/// <summary>
/// Request model for cancelling a item by ID
/// </summary>
public class CancelItemRequest
{
    /// <summary>
    /// The unique identifier of the sale to retrieve
    /// </summary>
    public Guid SaleId { get; set; }

    /// <summary>
    /// The unique identifier of the item to retrieve
    /// </summary>
    public Guid ItemId { get; set; }
}

