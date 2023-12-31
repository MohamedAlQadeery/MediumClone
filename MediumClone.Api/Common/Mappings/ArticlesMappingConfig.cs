using Mapster;
using MediumClone.Api.Contracts.Articles;
using MediumClone.Application.Articles.Commands;
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
        config.NewConfig<(string userId, CreateArticleRequest request), CreateArticleCommand>()
            .Map(dest => dest.AuthorId, src => src.userId)
            .Map(dest => dest, src => src.request);


        config.NewConfig<(int id, UpdateArticleRequest request), UpdateArticleCommand>()
      .Map(dest => dest.Id, src => src.id)
      .Map(dest => dest, src => src.request);

        config.NewConfig<Article, ArticleResponse>()
                .Map(dest => dest.TagNames, src => src.ArticleTags.Select(at => at.Tag.Name).ToList())
                .Map(dest => dest.Author.FullName, src => $"{src.Author.FirstName} {src.Author.LastName}")
                .Map(dest => dest.Author, src => src.Author);


        //config.NewConfig<ProductCategory, ProductCategoryResponse>()
        //    .Map(dest => dest.ProductsIds, src => src.Products.Select(x => x.Id).ToList());

    }
}