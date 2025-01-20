using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelItem;

/// <summary>
/// Profile for mapping CancelItem feature requests to commands
/// </summary>
public class CancelItemProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CancelItem feature
    /// </summary>
    public CancelItemProfile()
    {
        CreateMap<Guid, Application.Sales.CancelItem.CancelItemCommand>()
            .ConstructUsing(id => new Application.Sales.CancelItem.CancelItemCommand(id));
    }
}
