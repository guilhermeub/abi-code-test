using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

/// <summary>
/// Handler for processing CancelSaleCommand requests
/// </summary>
public class CancelSaleHandler : IRequestHandler<CancelSaleCommand, CancelSaleResponse>
{
    private readonly ISaleRepository _saleRepository;

    /// <summary>
    /// Initializes a new instance of CancelSaleHandler
    /// </summary>
    /// <param name="saleRepository">The sale repository</param>
    /// <param name="validator">The validator for CancelSaleCommand</param>
    public CancelSaleHandler(
        ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;
    }

    /// <summary>
    /// Handles the CancelSaleCommand request
    /// </summary>
    /// <param name="request">The CancelSale command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The result of the sale operation</returns>
    public async Task<CancelSaleResponse> Handle(CancelSaleCommand request, CancellationToken cancellationToken)
    {
        var validator = new CancelSaleValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var success = await _saleRepository.GetSaleByIdAsync(request.SaleId, cancellationToken);
        if (success == null)
            throw new KeyNotFoundException($"Sale with ID {request.SaleId} not found");

        await _saleRepository.DeleteSaleAsync(request.SaleId, cancellationToken);

        return new CancelSaleResponse { Success = true };
    }
}
