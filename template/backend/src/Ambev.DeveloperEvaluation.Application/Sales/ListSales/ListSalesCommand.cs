using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales;

/// <summary>
/// Command for retrieving a sale by their ID
/// </summary>
public record ListSalesCommand : IRequest<ListSalesResult>
{
    /// <summary>
    /// The unique identifier of the sale to retrieve
    /// </summary>
    public Guid CustomerId { get; }

    /// <summary>
    /// Initializes a new instance of ListSalesCommand
    /// </summary>
    /// <param name="id">The ID of the custoemr to retrieve the list</param>
    public ListSalesCommand(Guid id)
    {
        CustomerId = id;
    }
}
