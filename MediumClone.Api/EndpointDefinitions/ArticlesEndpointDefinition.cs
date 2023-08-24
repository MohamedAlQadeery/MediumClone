using System.Security.Claims;
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
        articles.MapGet("/your-feed", GetYourFeedArticles);

        articles.MapGet("/tag/{tagName}", GetAllArticlesByTag).AllowAnonymous();
        articles.MapGet("/{id}", GetArticleById).WithName("GetArticleById").AllowAnonymous();
        articles.MapDelete("/{slug}", DeleteArticle).WithName("DeleteArticle");
    }


    private async Task<IResult> CreateArticle(HttpContext context, ISender mediatr, IMapper mapper,
 CreateArticleRequest request)
    {
        var currentUserId = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

        var command = mapper.Map<CreateArticleCommand>((currentUserId, request));
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


        var result = await mediatr.Send(new GetAllArticlesQuery(mapper.Map<CommonQueryParams>(queryParams)));



        return TypedResults.Ok(mapper.Map<PaginatedList<ArticleResponse>>(result));
    }
    private async Task<IResult> GetAllArticlesByTag(ISender mediatr, IMapper mapper, string tagName,
     [AsParameters] QueryParamters queryParams)
    {


        var result = await mediatr.Send(new GetAllArticlesByTagQuery(tagName, queryParams.PageNumber ?? 1,
        queryParams.PageSize ?? 10));



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

    private async Task<IResult> GetYourFeedArticles(HttpContext context, ISender mediatr, IMapper mapper, [AsParameters] QueryParamters queryParams)
    {
        var currentUserId = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

        var result = await mediatr.Send(new GetYourFeedArticlesQuery(currentUserId!, mapper.Map<CommonQueryParams>(queryParams)));

        return TypedResults.Ok(mapper.Map<PaginatedList<ArticleResponse>>(result));
    }


    private async Task<IResult> DeleteArticle(HttpContext context, ISender mediatr, string slug)
    {
        var result = await mediatr.Send(new DeleteArticleCommand(slug));

        return result.Match(
            article => TypedResults.NoContent(),
            errors => ResultsProblem(context, errors)
        );
    }

}