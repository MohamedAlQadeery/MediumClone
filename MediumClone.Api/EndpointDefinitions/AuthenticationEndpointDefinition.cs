using MediumClone.Api.Abstractions;
using MapsterMapper;
using MediatR;
using MediumClone.Contracts.Authentication;
using MediumClone.Application.Authentication.Commands.Register;
using MediumClone.Application.Authentication.Queries.Login;
using MediumClone.Application.Authentication.Queries.GetCurrentUser;

namespace MediumClone.Api.EndpointDefinitions;
public class AuthenticationEndpointDefinition : BaseEndpointDefinition, IEndpointDefintion
{
    public void RegisterEndpoints(WebApplication app)
    {
        var auth = app.MapGroup("/api/auth");
        auth.MapPost("/register", Register).AllowAnonymous();
        auth.MapPost("/login", Login).AllowAnonymous();
        auth.MapGet("/user", GetCurrentUser);

        // // .AddEndpointFilter<ValidationFilter<CreateProductCategoryRequest>>();


    }



    private async Task<IResult> Register(HttpContext context, ISender mediatr, IMapper mapper, RegisterRequest request)
    {
        var command = mapper.Map<RegisterCommand>(request);
        var result = await mediatr.Send(command);

        return result.Match(
             response => TypedResults.Ok(mapper.Map<AuthenticationResponse>(response)),
              errors => ResultsProblem(context, errors)
          );
    }
    private async Task<IResult> Login(HttpContext context, ISender mediatr, IMapper mapper, LoginRequest request)
    {
        var command = mapper.Map<LoginQuery>(request);
        var result = await mediatr.Send(command);

        return result.Match(
             resposne => TypedResults.Ok(mapper.Map<AuthenticationResponse>(resposne)),
              errors => ResultsProblem(context, errors)
          );
    }


    private async Task<IResult> GetCurrentUser(HttpContext context, ISender mediatr, IMapper mapper)
    {

        var result = await mediatr.Send(new GetCurrentUserQuery(context));

        return result.Match(
             resposne => TypedResults.Ok(mapper.Map<AuthenticationResponse>(resposne)),
              errors => ResultsProblem(context, errors)
          );
    }


}
