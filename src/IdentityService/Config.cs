using Duende.IdentityServer.Models;

namespace IdentityService;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        [
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        ];

    public static IEnumerable<ApiScope> ApiScopes =>
        [
            new ApiScope("fashionPlace", "FashionPlace Access")
        ];

    public static IEnumerable<Client> Clients =>
        [
            new Client
            {
                ClientId = "postman",
                ClientName = "Postman",
                AllowedScopes = { "openid", "profile", "fashionPlace" },
                RedirectUris = { "https://www.postman.com/oauth2/callback" },
                ClientSecrets = [ new Secret("secret-postman".Sha256()) ],
                AllowedGrantTypes = { GrantType.ResourceOwnerPassword }
            },
            new Client
            {
                ClientId = "customerWeb",
                ClientName = "CustomerWeb",
                ClientSecrets = { new Secret("secret-customerWeb".Sha256()) },
                AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                RequirePkce = false,
                RedirectUris = { "http://localhost:3000/api/auth/callback/id-server" },
                AllowOfflineAccess = true,
                AllowedScopes = { "openid", "profile", "fashionPlace" },
                AccessTokenLifetime = 3600 * 24 * 30
            },
            new Client
            {
                ClientId = "businessDesktop",
                ClientName = "BusinessDesktop",
                ClientSecrets = { new Secret("secret-businessDesktop".Sha256()) },
                AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                RequirePkce = false,
                RedirectUris = { "myapp://auth/callback" },
                PostLogoutRedirectUris = { "myapp://auth/logout-callback" },
                AllowOfflineAccess = true,
                AllowedScopes = { "openid", "profile", "fashionPlace" },
                AccessTokenLifetime = 3600 * 24 * 30,
            }
        ];
}
