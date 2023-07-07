using MediumClone.Api.Abstractions;
using MapsterMapper;
using MediatR;
using MediumClone.Contracts.Authentication;
using MediumClone.Application.Authentication.Commands.Register;
using MediumClone.Application.Authentication.Queries.Login;

namespace MediumClone.Api.EndpointDefinitions;
public class AuthenticationEndpointDefinition : BaseEndpointDefinition, IEndpointDefintion
{
    public void RegisterEndpoints(WebApplication app)
    {
        var auth = app.MapGroup("/api/auth");
        auth.MapPost("/register", Register);
        auth.MapPost("/login", Login);
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


}
