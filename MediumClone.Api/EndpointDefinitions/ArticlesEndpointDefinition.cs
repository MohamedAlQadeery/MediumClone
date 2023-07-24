using MapsterMapper;
using MediatR;
using MediumClone.Api.Abstractions;
using MediumClone.Api.Contracts.Articles;
using MediumClone.Application.Articles.Commands;

namespace MediumClone.Api.EndpointDefinitions;
public class ArticlesEndpointDefinition : BaseEndpointDefinition, IEndpointDefintion
{
    public void RegisterEndpoints(WebApplication app)
    {
        var articles = app.MapGroup("/api/articles");
        articles.MapPost("/", CreateArticle);
    }


    private async Task<IResult> CreateArticle(HttpContext context, ISender mediatr, IMapper mapper,
 CreateArticleRequest request)
    {
        var command = mapper.Map<CreateArticleCommand>(request);
        var result = await mediatr.Send(command);

        return result.Match(
             //   tag => Results.CreatedAtRoute("GetTagById", new { id = tag.Id },
             //    mapper.Map<Tag>(tag)),
             artileCreated => TypedResults.Ok(mapper.Map<ArticleResponse>(artileCreated)),
              //productCategory => TypedResults.Ok(productCategory),
              errors => ResultsProblem(context, errors)
          );
    }
}