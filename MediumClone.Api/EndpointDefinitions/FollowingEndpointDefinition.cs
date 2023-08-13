using MediumClone.Api.Abstractions;
using MapsterMapper;
using MediatR;
using MediumClone.Contracts.Authentication;
using MediumClone.Application.Authentication.Commands.Register;
using MediumClone.Application.Authentication.Queries.Login;
using MediumClone.Application.Authentication.Queries.GetCurrentUser;
using MediumClone.Application.Followings.Commands;
using System.Security.Claims;
using MediumClone.Api.Contracts.Followings;
using MediumClone.Api.Common;
using MediumClone.Application.Followings.Queries;
using MediumClone.Application.Common;

namespace MediumClone.Api.EndpointDefinitions;
public class FollowingEndpointDefinition : BaseEndpointDefinition, IEndpointDefintion
{
    public void RegisterEndpoints(WebApplication app)
    {
        var auth = app.MapGroup("/api/followings/");
        auth.MapPost("/follow-user", FollowUser);
        auth.MapPost("/unfollow-user", UnFollowUser);
        auth.MapGet("/get-followings", GetFollowings);

        // // .AddEndpointFilter<ValidationFilter<CreateProductCategoryRequest>>();


    }



    private async Task<IResult> FollowUser(HttpContext context, ISender mediatr, IMapper mapper, FollowUserRequest request)
    {

        var currentUserId = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        var command = new FollowUserCommand(currentUserId, request.UserId);
        var result = await mediatr.Send(command);

        return result.Match(
             resposne => TypedResults.Ok(mapper.Map<FollowingResponse>(resposne)),
              errors => ResultsProblem(context, errors)
          );
    }
    private async Task<IResult> UnFollowUser(HttpContext context, ISender mediatr, IMapper mapper, UnFollowUserRequest request)
    {
        var currentUserId = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        var command = new UnFollowUserCommand(currentUserId, request.UserId);
        var result = await mediatr.Send(command);

        return result.Match(
             resposne => TypedResults.NoContent(),
              errors => ResultsProblem(context, errors)
          );
    }


    private async Task<IResult> GetFollowings(HttpContext context, ISender mediatr, IMapper mapper,
    [AsParameters] QueryParamters queryParams)
    {
        var currentUserId = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        var result = await mediatr.Send(new GetFollowingsQuery(currentUserId!, mapper.Map<CommonQueryParams>(queryParams)));

        return TypedResults.Ok(mapper.Map<PaginatedList<FollowingInfoResponse>>(result));
    }
}




