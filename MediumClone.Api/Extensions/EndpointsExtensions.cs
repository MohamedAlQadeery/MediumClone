using MediumClone.Api.Abstractions;

namespace MediumClone.Api.Extensions;

public static class EndpointExtensions
{
    public static void RegisterEndpointDefinitions(this WebApplication app)
    {
        var endpointDefinitions = typeof(Program).Assembly
            .GetTypes()
            .Where(x => x.IsAssignableTo(typeof(IEndpointDefintion)) && !x.IsInterface && !x.IsAbstract)
            .Select(Activator.CreateInstance)
            .Cast<IEndpointDefintion>();


        foreach (var endpointDefinition in endpointDefinitions)
        {
            endpointDefinition.RegisterEndpoints(app);
        }
    }
}