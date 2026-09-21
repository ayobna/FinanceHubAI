using FinanceHubAI.Api.Middleware;
using FinanceHubAI.Api.Services;
using FinanceHubAI.Application;
using FinanceHubAI.Application.Common.Interfaces;
using FinanceHubAI.Infrastructure;
 
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddOpenApi();

        builder.Services.AddApplication();
        builder.Services.AddInfrastructure(builder.Configuration);

        builder.Services.AddControllers();

        builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("Frontend", policy =>
            {
                policy
                    .WithOrigins("http://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        var app = builder.Build();

        app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseCors("Frontend");

        app.MapControllers();

        app.Run();