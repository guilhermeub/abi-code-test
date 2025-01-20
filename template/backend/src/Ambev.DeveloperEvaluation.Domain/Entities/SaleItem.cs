using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;


/// <summary>
/// Represents a sale in the system with authentication and profile information.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class SaleItem : BaseEntity, ISaleItem
{
    public string Product { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal AmountItemPrice { get; set; }
    public bool IsCancelled { get; set; } = false;

    public Guid SaleId { get; set; }
    public virtual Sale Sale { get; set; }

    // Method to cancel the item
    public void CancelItem()
    {
        IsCancelled = true;
        AmountItemPrice = 0m;
    }
}