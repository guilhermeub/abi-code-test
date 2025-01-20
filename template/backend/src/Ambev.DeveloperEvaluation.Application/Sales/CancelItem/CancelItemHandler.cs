using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelItem;

/// <summary>
/// Handler for processing CancelItemCommand requests
/// </summary>
public class CancelItemHandler : IRequestHandler<CancelItemCommand, CancelItemResponse>
{
    private readonly ISaleRepository _saleRepository;

    /// <summary>
    /// Initializes a new instance of CancelItemHandler
    /// </summary>
    /// <param name="saleRepository">The sale repository</param>
    /// <param name="validator">The validator for CancelItemCommand</param>
    public CancelItemHandler(
        ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;
    }

    /// <summary>
    /// Handles the CancelItemCommand request
    /// </summary>
    /// <param name="request">The CancelItem command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The result of the sale operation</returns>
    public async Task<CancelItemResponse> Handle(CancelItemCommand request, CancellationToken cancellationToken)
    {
        var validator = new CancelItemValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var success = await _saleRepository.GetSaleItemByIdAsync(request.SaleId, request.ItemId, cancellationToken);
        if (success == null)
            throw new KeyNotFoundException($"Sale with ID {request.SaleId} not found");

        await _saleRepository.CancelSaleItemAsync(request.SaleId, request.ItemId, cancellationToken);

        return new CancelItemResponse { Success = true };
    }
}
