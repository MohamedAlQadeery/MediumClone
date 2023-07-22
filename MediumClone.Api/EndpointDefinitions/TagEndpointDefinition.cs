using MediumClone.Api.Abstractions;
using MediumClone.Api.Contracts.ProductCategory;
using MediumClone.Api.Contracts.ProductCategory.Request;
using MediumClone.Application.ProductCategories.Commands;
using MediumClone.Application.ProductCategories.Commands.DeleteProductCategory;
using MediumClone.Application.ProductCategories.Queries;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MediumClone.Api.Contracts.Tags;
using MediumClone.Application.Tags.Commands;
using MediumClone.Domain.TagEntity;

namespace MediumClone.Api.EndpointDefinitions;
public class TagEndpointDefinition : BaseEndpointDefinition, IEndpointDefintion
{
    public void RegisterEndpoints(WebApplication app)
    {
        var tags = app.MapGroup("/api/tags");
        tags.MapPost("/", CreateTag);
        // // .AddEndpointFilter<ValidationFilter<CreateProductCategoryRequest>>();

        // categories.MapGet("", GetAllProductCategories);
        // categories.MapGet("/{id}", GetProductCategoryById).WithName("GetProductCategoryById");
        tags.MapPut("/{id}", UpdateTag);
        tags.MapDelete("/{id}", DeleteTag);
    }



    private async Task<IResult> CreateTag(HttpContext context, ISender mediatr, IMapper mapper,
    CreateTagRequest request)
    {
        var command = mapper.Map<CreateTagCommand>(request);
        var tag = await mediatr.Send(command);

        return tag.Match(
             //   tag => Results.CreatedAtRoute("GetTagById", new { id = tag.Id },
             //    mapper.Map<Tag>(tag)),
             tagCreated => TypedResults.Ok(tagCreated),
              //productCategory => TypedResults.Ok(productCategory),
              errors => ResultsProblem(context, errors)
          );
    }

    //update product category
    private async Task<IResult> UpdateTag(HttpContext context, IMapper mapper,
    ISender mediatr, int id, UpdateTagRequest request)
    {
        var command = mapper.Map<UpdateTagCommand>((request, id));
        var tagUpdatedResult = await mediatr.Send(command);

        return tagUpdatedResult.Match(
              //productCategory => Results.CreatedAtRoute("GetById", new { id = productCategory.Id }, productCategory),
              tagUpdated => TypedResults.Ok(tagUpdated),
              errors => ResultsProblem(context, errors)
          );
    }


    private async Task<IResult> DeleteTag(HttpContext context,
    ISender mediatr, int id)
    {
        var command = new DeleteTagCommand(id);
        var deleteResult = await mediatr.Send(command);

        return deleteResult.Match(
              //productCategory => Results.CreatedAtRoute("GetById", new { id = productCategory.Id }, productCategory),
              tagDeleted => TypedResults.NoContent(),
              errors => ResultsProblem(context, errors)
          );
    }



    private async Task<IResult> GetAllProductCategories(ISender mediatr, IMapper mapper)
    {
        var catgories = await mediatr.Send(new GetAllProductCategoriesQuery());

        return TypedResults.Ok(mapper.Map<IEnumerable<ProductCategoryResponse>>(catgories));
    }

    private async Task<IResult> GetProductCategoryById(HttpContext context, IMapper mapper, ISender mediatr, int id)
    {
        var categoryResult = await mediatr.Send(new GetProductCategoryByIdQuery(id));

        return categoryResult.Match(
            category => TypedResults.Ok(mapper.Map<ProductCategoryResponse>(category)),
            errors => ResultsProblem(context, errors)
        );
    }
}
