using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale;

/// <summary>
/// Profile for mapping CancelItem feature requests to commands
/// </summary>
public class CancelSaleProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CancelItem feature
    /// </summary>
    public CancelSaleProfile()
    {
        CreateMap<Guid, Application.Sales.CancelSale.CancelSaleCommand>()
            .ConstructUsing(id => new Application.Sales.CancelSale.CancelSaleCommand(id));
    }
}
