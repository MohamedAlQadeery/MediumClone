using MediumClone.Infrastructure;
using MediumClone.Application;
using MediumClone.Api.Extensions;
using MediumClone.Api;
using MediumClone.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
    .AddPresentaion()
    .AddApplication().AddInfrastructure(builder.Configuration);
}
var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }


    // Initialise and seed database
    using (var scope = app.Services.CreateScope())
    {
        var initialiser = scope.ServiceProvider.GetRequiredService<AppDbContextInitialiser>();
        await initialiser.InitialiseAsync();
        await initialiser.SeedAsync();
    }
    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseAuthentication();
    app.UseAuthorization();
    app.RegisterEndpointDefinitions();
    app.UseCors("AllowAllOrigins");





    app.Run();
}

