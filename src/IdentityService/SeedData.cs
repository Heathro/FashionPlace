using System.Security.Claims;
using IdentityModel;
using IdentityService.Data;
using IdentityService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IdentityService;

public class SeedData
{
    public static void EnsureSeedData(WebApplication app)
    {
        using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();

        var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        if (userMgr.Users.Any()) return;

        var admin = new ApplicationUser
        {
            UserName = "admin",
            Email = "admin@email.com",
            EmailConfirmed = true,
        };
        var result = userMgr.CreateAsync(admin, "Pass123$").Result;
        if (!result.Succeeded)
        {
            throw new Exception(result.Errors.First().Description);
        }
        result = userMgr.AddClaimsAsync(admin, [new Claim(JwtClaimTypes.Name, "Admin")]).Result;
        if (!result.Succeeded)
        {
            throw new Exception(result.Errors.First().Description);
        }
        Log.Debug("admin created");
    }
}
