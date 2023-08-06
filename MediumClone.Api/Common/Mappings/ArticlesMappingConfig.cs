using Mapster;
using MediumClone.Api.Contracts.Articles;
using MediumClone.Domain.ArticleEntity;

namespace MediumClone.Api.Common.Mappings;

public class ArticlesMappingConfig : IRegister
{

    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Article, ArticleResponse>()
                .Map(dest => dest.TagNames, src => src.ArticleTags.Select(at => at.Tag.Name).ToList())
                .Map(dest => dest.Author.FullName, src => $"{src.Author.FirstName} {src.Author.LastName}")
                .Map(dest => dest.Author, src => src.Author);











        //config.NewConfig<ProductCategory, ProductCategoryResponse>()
        //    .Map(dest => dest.ProductsIds, src => src.Products.Select(x => x.Id).ToList());

    }
}