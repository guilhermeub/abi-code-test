using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSales;

/// <summary>
/// Profile for mapping ListSales feature requests to commands
/// </summary>
public class ListSalesProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetSale feature
    /// </summary>
    public ListSalesProfile()
    {
        CreateMap<Guid, Application.Sales.ListSales.ListSalesCommand>()
            .ConstructUsing(id => new Application.Sales.ListSales.ListSalesCommand(id));
    }
}
