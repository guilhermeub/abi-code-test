using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class SaleValidator : AbstractValidator<Sale>
{
    public SaleValidator()
    {
        RuleFor(sale => sale.SaleNumber)
            .NotEmpty()
            .MinimumLength(3).WithMessage("SaleNumber must be at least 3 characters long.")
            .MaximumLength(50).WithMessage("SaleNumber cannot be longer than 50 characters.");
    }
}
