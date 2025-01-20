using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;


/// <summary>
/// Represents a sale in the system with authentication and profile information.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class Sale : BaseEntity, ISale
{
    /// <summary>
    /// Gets the sale's number.
    /// </summary>
    public string SaleNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets the Date and Time.
    /// </summary>
    public DateTime SaleDate { get; set; }

    /// <summary>
    /// Gets the Custoemr of the sale was created.
    /// </summary>
    public virtual User? Customer { get; set; }

    /// <summary>
    /// Gets the sale's total amount.
    /// </summary>
    public decimal TotalSaleAmount { get; set; }

    /// <summary>
    /// Gets the branch of the sale.
    /// </summary>
    public string Branch { get; set; } = string.Empty;

    /// <summary>
    /// Gets the status of the sale.
    /// </summary>
    public bool IsCancelled { get; set; } = false;

    public ICollection<SaleItem> Items { get; set; } = new List<SaleItem>();

    /// <summary>
    /// Performs validation of the sale entity using the SaleValidator rules.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> containing:
    /// - IsValid: Indicates whether all validation rules passed
    /// - Errors: Collection of validation errors if any rules failed
    /// </returns>
    /// <remarks>
    /// <listheader>The validation includes checking:</listheader>
    /// <list type="bullet">Salename format and length</list>
    /// <list type="bullet">Phone number format</list>
    /// <list type="bullet">Role validity</list>
    /// 
    /// </remarks>
    public ValidationResultDetail Validate()
    {
        var validator = new SaleValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}