using EmployeeHub.Server.DatabaseConfiguration;
using EmployeeHub.Server.EmpoyeeFeature;
using FastEndpoints;
using Scalar.AspNetCore;

namespace EmployeeHub.Server;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddOpenApi();
        builder.Services.AddFastEndpoints();

        builder.Services.AddSingleton<IDbConnectionFactory>(_ =>
            new SqlServerConnectionFactory(builder.Configuration["ConnectionString"]!));
        builder.Services.AddSingleton(_ => new DbInitializer(builder.Configuration["ConnectionString"]!));
        builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

        var app = builder.Build();

        app.UseDefaultFiles();
        app.MapStaticAssets();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference(options =>
            {
                options
                    .WithTitle("Employee API")
                    .WithTheme(ScalarTheme.DeepSpace)
                    .WithDefaultHttpClient(ScalarTarget.CSharp,ScalarClient.HttpClient);
            });
        }

        app.UseFastEndpoints(f =>
        {
            f.Endpoints.RoutePrefix = "api";
            f.Endpoints.Configurator = fc =>
            {
                fc.AllowAnonymous();
            };
        });

        app.UseHttpsRedirection();
        app.MapFallbackToFile("/index.html");

        var databaseInitializer = app.Services.GetRequiredService<DbInitializer>();
        await databaseInitializer.InitializeAsync();

        app.Run();
    }
}
