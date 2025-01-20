using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;
using System;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

/// <summary>
/// Command for cancelling an existing sale.
/// </summary>
/// <remarks>
/// This command is used to cancel an existing sale by setting its status to cancelled.
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
/// that returns a <see cref="CancelSaleResponse"/>.
/// </remarks>
public class CancelSaleCommand : IRequest<CancelSaleResponse>
{
    /// <summary>
    /// Gets or sets the sale ID to be cancelled.
    /// </summary>
    public Guid SaleId { get; set; }

    /// <summary>
    /// Gets or sets a flag indicating whether the sale is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; } = true;

    public CancelSaleCommand(Guid id)
    {
        SaleId = id;
    }
}
