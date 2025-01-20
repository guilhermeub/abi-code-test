using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelItem;

/// <summary>
/// Validator for CancelItemCommand
/// </summary>
public class CancelItemValidator : AbstractValidator<CancelItemCommand>
{
    /// <summary>
    /// Initializes validation rules for CancelItemCommand
    /// </summary>
    public CancelItemValidator()
    {
        RuleFor(x => x.ItemId)
            .NotEmpty()
            .WithMessage("Item ID is required");
    }
}
