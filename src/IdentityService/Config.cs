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
            new ApiScope("catalogService", "CatalogService Access")
        ];

    public static IEnumerable<Client> Clients =>
        [
            new Client
            {
                ClientId = "postman",
                ClientName = "Postman",
                AllowedScopes = { "openid", "profile", "catalogService" },
                RedirectUris = { "https://www.postman.com/oauth2/callback" },
                ClientSecrets = [ new Secret("NotSoSecret".Sha256()) ],
                AllowedGrantTypes = { GrantType.ResourceOwnerPassword }
            }
        ];
}
