
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MediumClone.Api.Common.Mappings;
using MediumClone.Api.Abstractions;
using MediumClone.Api.Common.Errors;
using MediumClone.Api.Common.Services;
using MediumClone.Api.Extensions;

namespace MediumClone.Api;

public static class DependancyInjection
{

    public static IServiceCollection AddPresentaion(this IServiceCollection services)
    {
        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddMappings();
        services.AddValidationFilter();
        services.AddSingleton<ProblemDetailsFactory, SalesAppProblemDetailsFactory>();
        services.AddScoped<IImageService, ImageService>();
        services.AddAuthentication();
        services.AddAuthorization();


        return services;
    }
}