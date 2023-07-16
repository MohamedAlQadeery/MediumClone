
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MediumClone.Api.Common.Mappings;
using MediumClone.Api.Abstractions;
using MediumClone.Api.Common.Errors;
using MediumClone.Api.Common.Services;
using MediumClone.Api.Extensions;
using Microsoft.OpenApi.Models;

namespace MediumClone.Api;

public static class DependancyInjection
{

    public static IServiceCollection AddPresentaion(this IServiceCollection services)
    {
        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    // Define the JWT security scheme
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    // Add the JWT security requirement to all endpoints
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

        services.AddMappings();
        services.AddValidationFilter();
        services.AddSingleton<ProblemDetailsFactory, SalesAppProblemDetailsFactory>();
        services.AddScoped<IImageService, ImageService>();

        services.AddCors(options =>
           {
               options.AddPolicy("AllowAllOrigins",
                   builder =>
                   {
                       builder.AllowAnyOrigin()
                              .AllowAnyMethod()
                              .AllowAnyHeader();
                   });
           });

        return services;
    }
}