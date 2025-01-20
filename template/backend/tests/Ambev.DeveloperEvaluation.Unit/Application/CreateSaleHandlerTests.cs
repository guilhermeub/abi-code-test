using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Domain;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

/// <summary>
/// Contains unit tests for the <see cref="CreateSaleHandler"/> class.
/// </summary>
public class CreateSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly CreateSaleHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateSaleHandlerTests"/> class.
    /// Sets up the test dependencies and creates fake data generators.
    /// </summary>
    public CreateSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new CreateSaleHandler(_saleRepository, _mapper);
    }

    /// <summary>
    /// Tests that a valid sale creation request is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid sale data When creating sale Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var command = CreateSaleHandlerTestData.GenerateValidCommand();
        var sale = new Sale
        {
            Id = Guid.NewGuid(),
            SaleNumber = command.SaleNumber,
            SaleDate = command.SaleDate,
            Customer = new User() { Username = command.Customer },
            TotalSaleAmount = command.TotalSaleAmount,
            Branch = command.Branch,
            IsCancelled = command.IsCancelled,
            Items = command.Items,
        };

        var result = new CreateSaleResult
        {
            SaleId = sale.Id,
        };


        _mapper.Map<Sale>(command).Returns(sale);
        _mapper.Map<CreateSaleResult>(sale).Returns(result);

        _saleRepository.CreateSaleAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
            .Returns(sale);

        // When
        var CreateSaleResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        CreateSaleResult.Should().NotBeNull();
        CreateSaleResult.SaleId.Should().Be(sale.Id);
        await _saleRepository.Received(1).CreateSaleAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that an invalid sale creation request throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid sale data When creating sale Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new CreateSaleCommand(); // Empty command will fail validation

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    /// <summary>
    /// Tests that the mapper is called with the correct command.
    /// </summary>
    [Fact(DisplayName = "Given valid command When handling Then maps command to sale entity")]
    public async Task Handle_ValidRequest_MapsCommandToSale()
    {
        // Given
        var command = CreateSaleHandlerTestData.GenerateValidCommand();
        var sale = new Sale
        {
            Id = Guid.NewGuid(),
            SaleNumber = command.SaleNumber,
            SaleDate = command.SaleDate,
            Customer = new User() { Username = command.Customer },
            TotalSaleAmount = command.TotalSaleAmount,
            Branch = command.Branch,
            IsCancelled = command.IsCancelled,
            Items = command.Items,
        };

        _mapper.Map<Sale>(command).Returns(sale);
        _saleRepository.CreateSaleAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
            .Returns(sale);

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        _mapper.Received(1).Map<Sale>(Arg.Is<CreateSaleCommand>(c =>
            c.SaleNumber == command.SaleNumber &&
            c.SaleDate == command.SaleDate &&
            c.Customer == command.Customer &&
            c.TotalSaleAmount == command.TotalSaleAmount &&
            c.Branch == command.Branch &&
            c.IsCancelled == command.IsCancelled &&
            c.Items == command.Items));
    }

    [Fact(DisplayName = "Given sale item with quantity exceeding limit When creating sale Then throws validation exception")]
    public async Task Handle_ItemQuantityExceedsMax_ThrowsValidationException()
    {
        // Given
        var command = CreateSaleHandlerTestData.GenerateValidCommand();
        command.Items[0].Quantity = 25;

        var sale = new Sale
        {
            Id = Guid.NewGuid(),
            SaleNumber = command.SaleNumber,
            SaleDate = command.SaleDate,
            Customer = new User() { Username = command.Customer },
            TotalSaleAmount = command.TotalSaleAmount,
            Branch = command.Branch,
            IsCancelled = command.IsCancelled,
            Items = command.Items,
        };

        _mapper.Map<Sale>(command).Returns(sale);

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>()
            .WithMessage($"Cannot sell more than 20 units of {command.Items[0].Product}.");
    }

    [Fact(DisplayName = "Given sale item with valid quantity When creating sale Then discount is applied correctly")]
    public async Task Handle_ItemDiscountIsAppliedCorrectly()
    {
        // Given
        var command = CreateSaleHandlerTestData.GenerateValidCommand();
        command.Items[0].Quantity = 15;

        var sale = new Sale
        {
            Id = Guid.NewGuid(),
            SaleNumber = command.SaleNumber,
            SaleDate = command.SaleDate,
            Customer = new User() { Username = command.Customer },
            TotalSaleAmount = command.TotalSaleAmount,
            Branch = command.Branch,
            IsCancelled = command.IsCancelled,
            Items = command.Items,
        };

        var result = new CreateSaleResult
        {
            SaleId = sale.Id,
        };

        _mapper.Map<Sale>(command).Returns(sale);
        _mapper.Map<CreateSaleResult>(sale).Returns(result);

        _saleRepository.CreateSaleAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
                .Returns(sale);

        // When
        var saleResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        command.Items[0].Discount.Should().Be(0.20m);
        command.Items[0].AmountItemPrice.Should().Be(command.Items[0].Quantity * command.Items[0].UnitPrice * (1 - 0.20m));
    }

    [Fact(DisplayName = "Given sale items When creating sale Then total sale amount is calculated correctly")]
    public async Task Handle_SaleAmountCalculation_IsCorrect()
    {
        // Given
        var command = CreateSaleHandlerTestData.GenerateValidCommand();
        var totalAmountBeforeDiscount = command.Items.Sum(i => i.Quantity * i.UnitPrice);
        var totalAmountAfterDiscount = command.Items.Sum(i => i.AmountItemPrice);

        var sale = new Sale
        {
            Id = Guid.NewGuid(),
            SaleNumber = command.SaleNumber,
            SaleDate = command.SaleDate,
            Customer = new User() { Username = command.Customer },
            TotalSaleAmount = command.TotalSaleAmount,
            Branch = command.Branch,
            IsCancelled = command.IsCancelled,
            Items = command.Items,
        };

        var result = new CreateSaleResult
        {
            SaleId = sale.Id,
        };

        _mapper.Map<Sale>(command).Returns(sale);
        _mapper.Map<CreateSaleResult>(sale).Returns(result);

        _saleRepository.CreateSaleAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
        .Returns(sale);

        // When
        var saleResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        saleResult.TotalSaleAmount.Should().Be(totalAmountAfterDiscount);
    }


}
