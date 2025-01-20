using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelItem;

/// <summary>
/// Validator for CancelItemRequest
/// </summary>
public class CancelItemRequestValidator : AbstractValidator<CancelItemRequest>
{
    /// <summary>
    /// Initializes validation rules for CancelItemRequest
    /// </summary>
    public CancelItemRequestValidator()
    {
        RuleFor(x => x.ItemId)
            .NotEmpty()
            .WithMessage("Item ID is required");

        RuleFor(x => x.SaleId)
           .NotEmpty()
           .WithMessage("Sale ID is required");
    }
}
