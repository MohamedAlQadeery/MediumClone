using MapsterMapper;
using MediatR;
using MediumClone.Api.Abstractions;
using MediumClone.Api.Common;
using MediumClone.Api.Contracts.Articles;
using MediumClone.Application.Articles.Commands;
using MediumClone.Application.Articles.Queries;
using MediumClone.Application.Common;
using Microsoft.AspNetCore.Mvc;

namespace MediumClone.Api.EndpointDefinitions;
public class ArticlesEndpointDefinition : BaseEndpointDefinition, IEndpointDefintion
{
    public void RegisterEndpoints(WebApplication app)
    {
        var articles = app.MapGroup("/api/articles");
        articles.MapPost("/", CreateArticle);
        articles.MapGet("", GetAllArticles).AllowAnonymous();
        articles.MapGet("/{id}", GetArticleById).WithName("GetArticleById").AllowAnonymous();
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


    private async Task<IResult> GetAllArticles(ISender mediatr, IMapper mapper, [AsParameters] QueryParamters queryParams)
    {
        var result = await mediatr.Send(new GetAllArticlesQuery(queryParams.PageNumber, queryParams.PageSize));



        return TypedResults.Ok(mapper.Map<PaginatedList<ArticleResponse>>(result));
    }

    private async Task<IResult> GetArticleById(HttpContext context, IMapper mapper, ISender mediatr, int id)
    {
        var result = await mediatr.Send(new GetArticleByIdQuery(id));

        return result.Match(
            article => TypedResults.Ok(mapper.Map<ArticleResponse>(article)),
            errors => ResultsProblem(context, errors)
        );
    }
}