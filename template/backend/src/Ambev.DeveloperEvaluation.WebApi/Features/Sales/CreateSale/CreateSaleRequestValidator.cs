using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Validator for CreateSaleRequest that defines validation rules for user creation.
/// </summary>
public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
{
    /// <summary>
    /// Initializes a new instance of the CreateSaleRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Email: Must be valid format (using EmailValidator)
    /// - Salename: Required, length between 3 and 50 characters
    /// - Status: Cannot be Unknown
    /// </remarks>
    public CreateSaleRequestValidator()
    {
        RuleFor(user => user.Email).SetValidator(new EmailValidator());
        RuleFor(user => user.Salename).NotEmpty().Length(3, 50);
        RuleFor(user => user.Phone).Matches(@"^\+?[1-9]\d{1,14}$");
    }
}