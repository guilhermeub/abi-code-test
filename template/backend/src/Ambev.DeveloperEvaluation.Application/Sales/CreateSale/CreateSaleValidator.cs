using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Validator for CreateSaleCommand that defines validation rules for sale creation command.
/// </summary>
public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
{
    /// <summary>
    /// Initializes a new instance of the CreateSaleCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Salename: Required, must be between 3 and 50 characters
    /// - Status: Cannot be set to Unknown
    /// - Role: Cannot be set to None
    /// </remarks>
    public CreateSaleCommandValidator()
    {
        RuleFor(sale => sale.SaleNumber).NotEmpty().Length(3, 50);
        RuleFor(sale => sale.TotalSaleAmount).NotNull();
    }
}