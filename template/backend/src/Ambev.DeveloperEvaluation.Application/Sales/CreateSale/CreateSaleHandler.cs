using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Handler for processing CreateSaleCommand requests
/// </summary>
public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of CreateSaleHandler
    /// </summary>
    /// <param name="saleRepository">The sale repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="validator">The validator for CreateSaleCommand</param>
    public CreateSaleHandler(ISaleRepository saleRepository, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the CreateSaleCommand request
    /// </summary>
    /// <param name="command">The CreateSale command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created sale details</returns>
    public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        foreach (var item in command.Items)
        {
            if (item.Quantity > 20)
                throw new ValidationException($"Cannot sell more than 20 units of {item.Product}.");

            if (item.Quantity >= 10 && item.Quantity <= 20)
            {
                item.Discount = 0.20m; 
            }
            else if (item.Quantity >= 4 && item.Quantity < 10)
            {
                item.Discount = 0.10m; 
            }
            else
            {
                item.Discount = 0.00m;
            }

            item.AmountItemPrice = item.Quantity * item.UnitPrice * (1 - item.Discount);
        }

        var sale = _mapper.Map<Sale>(command);
        sale.TotalSaleAmount = command.Items.Sum(i => i.AmountItemPrice);

        var createdSale = await _saleRepository.CreateSaleAsync(sale, cancellationToken);
        var result = _mapper.Map<CreateSaleResult>(createdSale);
        return result;
    }
}
