using Mapster;
using MediumClone.Api.Contracts.Articles;
using MediumClone.Domain.ArticleEntity;

namespace MediumClone.Api.Common.Mappings;

public class ArticlesMappingConfig : IRegister
{

    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Article, ArticleResponse>()
                .Map(dest => dest.TagsId, src => src.ArticleTags.Select(at => at.TagId).ToList());





        //config.NewConfig<ProductCategory, ProductCategoryResponse>()
        //    .Map(dest => dest.ProductsIds, src => src.Products.Select(x => x.Id).ToList());

    }
}