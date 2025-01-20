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
    public decimal Price { get; set; }
    public bool IsCancelled { get; set; } = false;

    public Guid SaleId { get; set; }
    public virtual Sale Sale { get; set; }

    // Constructor
    public SaleItem(Guid saleId, string product, int quantity, decimal unitPrice)
    {
        SaleId = saleId;
        Product = product;
        Quantity = quantity;
        UnitPrice = unitPrice;
        CalculateTotalAmount();
    }

    // Method to calculate total amount for the item
    public void CalculateTotalAmount()
    {
        Price = Quantity * UnitPrice;
    }

    // Method to apply discount on the item
    public void ApplyDiscount()
    {
        if (Quantity >= 4 && Quantity < 10)
        {
            Discount = Price * 0.1m; // 10% discount
        }
        else if (Quantity >= 10 && Quantity <= 20)
        {
            Discount = Price * 0.2m; // 20% discount
        }
        else
        {
            Discount = 0m; // No discount
        }
        Price -= Discount;
    }

    // Method to cancel the item
    public void CancelItem()
    {
        IsCancelled = true;
        Price = 0m;
    }
}