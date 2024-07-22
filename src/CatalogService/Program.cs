using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MassTransit;
using CatalogService.Data;
using CatalogService.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers();

builder.Services
    .AddDbContext<CatalogDbContext>(options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresDb"));
    });

builder.Services
    .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services
    .AddMassTransit(x =>
    {
        x.AddEntityFrameworkOutbox<CatalogDbContext>(options =>
        {
            options.QueryDelay = TimeSpan.FromSeconds(10);
            options.UsePostgres();
            options.UseBusOutbox();
        });
        x.UsingRabbitMq((context, config) =>
        {
            config.Host(builder.Configuration["RabbitMq:Host"], "/", host =>
            {
                host.Username(builder.Configuration.GetValue("RabbitMq:Username", "guest"));
                host.Password(builder.Configuration.GetValue("RabbitMq:Password", "guest"));
            });
            config.ConfigureEndpoints(context);
        });
    });

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["IdentityServiceUrl"];
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters.ValidateAudience = false;
        options.TokenValidationParameters.NameClaimType = "username";
    });
    
builder.Services
    .AddScoped<IUnitOfWork, UnitOfWork>();
    
var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

DbInitializer.InitDb(app);

app.Run();

public partial class Program {}
