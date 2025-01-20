using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

/// <summary>
/// Profile for mapping UpdateSale feature requests to commands
/// </summary>
public class UpdateSaleProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for UpdateSale feature
    /// </summary>
    public UpdateSaleProfile()
    {
        CreateMap<Guid, Application.Sales.UpdateSale.UpdateSaleCommand>()
            .ConstructUsing(id => new Application.Sales.UpdateSale.UpdateSaleCommand(id));
    }
}
