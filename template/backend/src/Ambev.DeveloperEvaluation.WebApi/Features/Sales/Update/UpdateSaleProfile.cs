using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
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
        CreateMap<Guid, UpdateSaleCommand>()
            .ConstructUsing(id => new UpdateSaleCommand(id));

        CreateMap<UpdateSaleCommand, Sale>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
    }
}
