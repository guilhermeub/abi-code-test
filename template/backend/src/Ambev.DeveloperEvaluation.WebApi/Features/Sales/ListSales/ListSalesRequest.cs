namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

/// <summary>
/// Request model for getting a user by ID
/// </summary>
public class ListSalesRequest
{
    /// <summary>
    /// The unique identifier of the user to retrieve
    /// </summary>
    public Guid CustomerId { get; set; }
}
