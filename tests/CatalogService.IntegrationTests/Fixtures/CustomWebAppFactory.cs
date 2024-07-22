using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;
using MassTransit;
using CatalogService.Data;
using WebMotions.Fake.Authentication.JwtBearer;

namespace CatalogService.IntegrationTests;

public class CustomWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgresSqlContainer = new PostgreSqlBuilder().Build();

    public async Task InitializeAsync()
    {
        await _postgresSqlContainer.StartAsync();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services
                .RemoveDbContext<CatalogDbContext>();

            services
                .AddDbContext<CatalogDbContext>(options =>
                {
                    options.UseNpgsql(_postgresSqlContainer.GetConnectionString());
                });

            services
                .AddMassTransitTestHarness();

            services
                .EnsureCreated<CatalogDbContext>();

            services
                .AddAuthentication(FakeJwtBearerDefaults.AuthenticationScheme)
                .AddFakeJwtBearer(options =>
                {
                    options.BearerValueType = FakeJwtBearerBearerValueType.Jwt;
                });
        });
    }

    Task IAsyncLifetime.DisposeAsync()
    {
        return _postgresSqlContainer.DisposeAsync().AsTask();
    }
}
