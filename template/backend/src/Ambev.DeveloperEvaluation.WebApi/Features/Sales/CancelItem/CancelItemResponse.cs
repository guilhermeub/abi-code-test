namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelItem;

/// <summary>
/// API response model for CancelItem operation
/// </summary>
public class CancelItemResponse
{
    /// <summary>
    /// The unique identifier of the user
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The user's full name
    /// </summary>
    public string Name { get; set; } = string.Empty;
}
