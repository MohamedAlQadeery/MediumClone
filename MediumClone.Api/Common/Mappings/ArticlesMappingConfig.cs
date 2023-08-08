using Mapster;
using MediumClone.Api.Contracts.Articles;
using MediumClone.Application.Common;
using MediumClone.Domain.ArticleEntity;

namespace MediumClone.Api.Common.Mappings;

public class ArticlesMappingConfig : IRegister
{
    private const int _defaultPageNumber = 1;
    private const int _defaultPageSize = 10;
    private const string _defaultSortColumn = "id";
    private const string _defaultSortOrder = "desc";

    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Article, ArticleResponse>()
                .Map(dest => dest.TagNames, src => src.ArticleTags.Select(at => at.Tag.Name).ToList())
                .Map(dest => dest.Author.FullName, src => $"{src.Author.FirstName} {src.Author.LastName}")
                .Map(dest => dest.Author, src => src.Author);



        config.NewConfig<QueryParamters, CommonQueryParams>()
            .Map(dest => dest.PageNumber, src => src.PageNumber ?? _defaultPageNumber)
            .Map(dest => dest.PageSize, src => src.PageSize ?? _defaultPageSize)
            .Map(dest => dest.Search, src => src.Search ?? string.Empty)
            .Map(dest => dest.SortColumn, src => src.SortColumn ?? _defaultSortColumn)
            .Map(dest => dest.SortOrder, src => src.SortOrder ?? _defaultSortOrder);











        //config.NewConfig<ProductCategory, ProductCategoryResponse>()
        //    .Map(dest => dest.ProductsIds, src => src.Products.Select(x => x.Id).ToList());

    }
}