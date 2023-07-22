using Mapster;
using MediumClone.Api.Contracts.Tags;
using MediumClone.Application.Tags.Commands;

namespace MediumClone.Api.Common.Mappings;

public class TagMappingConfig : IRegister
{

    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateTagRequest, CreateTagCommand>();
        config.NewConfig<(UpdateTagRequest Request, int id), UpdateTagCommand>()
            .Map(dest => dest.Id, src => src.id)
            .Map(dest => dest, src => src.Request);


        //config.NewConfig<ProductCategory, ProductCategoryResponse>()
        //    .Map(dest => dest.ProductsIds, src => src.Products.Select(x => x.Id).ToList());

    }
}