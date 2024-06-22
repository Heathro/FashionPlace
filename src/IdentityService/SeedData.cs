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
        var adminResult = userMgr.CreateAsync(admin, "Pass123$").Result;
        if (!adminResult.Succeeded)
        {
            throw new Exception(adminResult.Errors.First().Description);
        }
        adminResult = userMgr.AddClaimsAsync(admin, [new Claim(JwtClaimTypes.Name, "Admin")]).Result;
        if (!adminResult.Succeeded)
        {
            throw new Exception(adminResult.Errors.First().Description);
        }
        Log.Debug("admin created");

        var user = new ApplicationUser
        {
            UserName = "user",
            Email = "user@email.com",
            EmailConfirmed = true,
        };
        var userResult = userMgr.CreateAsync(user, "Pass123$").Result;
        if (!userResult.Succeeded)
        {
            throw new Exception(userResult.Errors.First().Description);
        }
        userResult = userMgr.AddClaimsAsync(user, [new Claim(JwtClaimTypes.Name, "User")]).Result;
        if (!userResult.Succeeded)
        {
            throw new Exception(userResult.Errors.First().Description);
        }
        Log.Debug("user created");
    }
}
