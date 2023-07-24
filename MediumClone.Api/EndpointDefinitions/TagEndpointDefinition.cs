using MediumClone.Api.Abstractions;
using MediumClone.Application.Tags.Commands;
using MediumClone.Application.Tags.Queries;
using MapsterMapper;
using MediatR;
using MediumClone.Api.Contracts.Tags;

namespace MediumClone.Api.EndpointDefinitions;
public class TagEndpointDefinition : BaseEndpointDefinition, IEndpointDefintion
{
    public void RegisterEndpoints(WebApplication app)
    {
        var tags = app.MapGroup("/api/tags");
        tags.MapPost("/", CreateTag);
        // // .AddEndpointFilter<ValidationFilter<CreateProductCategoryRequest>>();

        tags.MapGet("", GetAllTags).AllowAnonymous();
        tags.MapGet("/{id}", GetTagById).WithName("GetTagById").AllowAnonymous();
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



    private async Task<IResult> GetAllTags(ISender mediatr, IMapper mapper)
    {
        var tags = await mediatr.Send(new GetAllTagsQuery());

        return TypedResults.Ok(tags);
    }

    private async Task<IResult> GetTagById(HttpContext context, IMapper mapper, ISender mediatr, int id)
    {
        var result = await mediatr.Send(new GetTagByIdQuery(id));

        return result.Match(
            tag => TypedResults.Ok(tag),
            errors => ResultsProblem(context, errors)
        );
    }
}
