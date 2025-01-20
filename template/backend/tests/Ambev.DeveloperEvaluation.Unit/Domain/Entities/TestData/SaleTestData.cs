using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class SaleTestData
{
    /// <summary>
    /// Configures the Faker to generate valid CreateSaleCommand entities.
    /// The generated sales will have valid:
    /// - SaleNumber (using alphanumeric string)
    /// - Customer (randomized customer name)
    /// - IsCancelled (set to false)
    /// - Branch (random company name)
    /// - SaleDate (current or past date)
    /// - TotalSaleAmount (random positive amount)
    /// - Items (randomized list of sale items)
    /// </summary>
    private static readonly Faker<Sale> SaleFaker = new Faker<Sale>()
        .RuleFor(c => c.SaleNumber, f => f.Random.AlphaNumeric(10))
        .RuleFor(c => c.Customer, f => new User())
        .RuleFor(c => c.IsCancelled, f => false)
        .RuleFor(c => c.Branch, f => f.Company.CompanyName())
        .RuleFor(c => c.SaleDate, f => f.Date.Past(1))
        .RuleFor(c => c.TotalSaleAmount, f => f.Finance.Amount(10, 5000, 2))
        .RuleFor(c => c.Items, f => GenerateSaleItems(f));

    /// <summary>
    /// Generates a list of sale items with randomized data.
    /// </summary>
    private static List<SaleItem> GenerateSaleItems(Faker f)
    {
        return f.Make(3, () => new SaleItem
        {
            SaleId = f.Random.Guid(),
            Product = f.Random.String(),
            Quantity = f.Random.Int(1, 5),
            UnitPrice = f.Finance.Amount(10, 500),
        }).ToList();
    }

    /// <summary>
    /// Generates a valid Sale entity with randomized data.
    /// The generated user will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid User entity with randomly generated data.</returns>
    public static Sale GenerateValidSale()
    {
        return SaleFaker.Generate();
    }

    public static string GenerateLongSaleNumber()
    {
        return new Faker().Random.String2(51);
    }
}
