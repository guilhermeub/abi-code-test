using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Entities;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Command for updating an existing sale.
/// </summary>
/// <remarks>
/// This command is used to update the details of an existing sale, 
/// such as customer, total amount, sale items, etc. It implements 
/// <see cref="IRequest{TResponse}"/> to initiate the request 
/// that returns an <see cref="UpdateSaleResult"/>.
/// </remarks>
public class UpdateSaleCommand : IRequest<UpdateSaleResult>
{
    /// <summary>
    /// Gets or sets the sale ID to be updated.
    /// </summary>
    public Guid SaleId { get; set; }

    /// <summary>
    /// Gets or sets the sale number (optional).
    /// </summary>
    public string SaleNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the customer for the sale.
    /// </summary>
    public string Customer { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the total amount of the sale.
    /// </summary>
    public decimal TotalSaleAmount { get; set; }

    /// <summary>
    /// Gets or sets the branch where the sale was made.
    /// </summary>
    public string Branch { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the list of updated sale items.
    /// </summary>
    public List<SaleItem> Items { get; set; } = new List<SaleItem>();

    /// <summary>
    /// Validates the update sale command.
    /// </summary>
    /// <returns>Validation result.</returns>
    public ValidationResultDetail Validate()
    {
        var validator = new UpdateSaleCommandValidator();
        var result = validator.Validate(this);

        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }

    public UpdateSaleCommand(Guid saleId)
    {
        SaleId = saleId;
    }

}
