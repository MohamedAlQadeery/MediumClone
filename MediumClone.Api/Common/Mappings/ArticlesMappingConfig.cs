using Mapster;
using MediumClone.Api.Contracts.Articles;
using MediumClone.Application.Common;
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



        config.NewConfig<QueryParamters, CommonQueryParams>()
            .Map(dest => dest.PageNumber, src => src.PageNumber ?? 1)
            .Map(dest => dest.PageSize, src => src.PageSize ?? 10)
            .Map(dest => dest.Search, src => src.Search ?? string.Empty)
            .Map(dest => dest.SortColumn, src => src.SortColumn ?? "id")
            .Map(dest => dest.SortOrder, src => src.SortOrder ?? "desc");











        //config.NewConfig<ProductCategory, ProductCategoryResponse>()
        //    .Map(dest => dest.ProductsIds, src => src.Products.Select(x => x.Id).ToList());

    }
}