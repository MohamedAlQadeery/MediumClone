using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols;
using MediumClone.Application.Abstractions.Repositories;
using MediumClone.Domain.AppUserEntity;
using MediumClone.Infrastructure.Persistence;
using MediumClone.Infrastructure.Persistence.Repositories;
using MediumClone.Infrastructure.Authentication;
using Microsoft.Extensions.Options;
using MediumClone.Application.Common.Interfaces.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MediumClone.Application.Common.Interfaces;
using MediumClone.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;

namespace MediumClone.Infrastructure;

public static class DependencyInjection
{


    public static IServiceCollection AddInfrastructure(
       this IServiceCollection services,
       ConfigurationManager configuration
    )
    {


        services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<AppDbContextInitialiser>();

        // Add Identity services
        services.AddIdentity<AppUser, IdentityRole>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 6;
            options.SignIn.RequireConfirmedEmail = false;
        })
        .AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultTokenProviders();


        services
              .AddAuth(configuration)
            .AddPersistance();

        return services;
    }
    public static IServiceCollection AddPersistance(
       this IServiceCollection services)
    {

        //services.AddScoped<PublishDomainEventsInterceptor>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IIdentityService, IdentityService>();

        // services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();


        return services;
    }



    public static IServiceCollection AddAuth(
            this IServiceCollection services,
            ConfigurationManager configuration)
    {
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);

        services.AddSingleton(Options.Create(jwtSettings));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings.Secret)),
            });

        services.AddAuthorization(options =>
        {
            //fallback policy jwt
            options.FallbackPolicy = new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build();



        });
        return services;
    }
}