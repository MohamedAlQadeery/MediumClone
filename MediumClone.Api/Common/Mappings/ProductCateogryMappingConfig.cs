
using Azure.Core;
using Mapster;
using MediumClone.Api.Contracts.ProductCategory;
using MediumClone.Api.Contracts.ProductCategory.Request;
using MediumClone.Application.ProductCategories.Commands;
using MediumClone.Domain.ProductCategoryEntity;

namespace DinnerNet.Api.Common.Mappings;

public class ProductCateogryMappingConfig : IRegister
{

    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateProductCategoryRequest, CreateProductCategoryCommad>();
        config.NewConfig<(UpdateProductCategoryRequest Request, int id), UpdateProductCategoryCommand>()
            .Map(dest => dest.Id, src => src.id)
            .Map(dest => dest, src => src.Request);


        //config.NewConfig<ProductCategory, ProductCategoryResponse>()
        //    .Map(dest => dest.ProductsIds, src => src.Products.Select(x => x.Id).ToList());

    }
}
