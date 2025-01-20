using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleCommandValidator : AbstractValidator<UpdateSaleCommand>
    {
        public UpdateSaleCommandValidator()
        {
            RuleFor(x => x.TotalSaleAmount).GreaterThan(0).WithMessage("Total Amount must be greater than zero.");
            RuleFor(x => x.Items).NotEmpty().WithMessage("At least one product is required.");
        }
    }
}
