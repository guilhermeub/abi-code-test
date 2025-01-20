using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

/// <summary>
/// Contains unit tests for the SaleValidator class.
/// Tests cover validation of all sale properties including saleNumber
/// </summary>
public class SaleValidatorTests
{
    private readonly SaleValidator _validator;

    public SaleValidatorTests()
    {
        _validator = new SaleValidator();
    }

    [Fact(DisplayName = "Valid sale should pass all validation rules")]
    public void Given_ValidUser_When_Validated_Then_ShouldNotHaveErrors()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();

        // Act
        var result = _validator.TestValidate(sale);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory(DisplayName = "Invalid saleNumber formats should fail validation")]
    [InlineData("")] // Empty
    [InlineData("ab")] // Less than 3 characters
    public void Given_InvalidSaleNumber_When_Validated_Then_ShouldHaveError(string saleNumber)
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();
        sale.SaleNumber = saleNumber;

        // Act
        var result = _validator.TestValidate(sale);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.SaleNumber);
    }

    [Fact(DisplayName = "SaleNumber longer than maximum length should fail validation")]
    public void Given_UsernameLongerThanMaximum_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();
        sale.SaleNumber = SaleTestData.GenerateLongSaleNumber();

        // Act
        var result = _validator.TestValidate(sale);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.SaleNumber);
    }
}
