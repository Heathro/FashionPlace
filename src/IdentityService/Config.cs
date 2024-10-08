﻿using Duende.IdentityServer.Models;

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
                ClientId = "customer-web",
                ClientName = "Customer-Web",
                ClientSecrets = { new Secret("secret-customer-web".Sha256()) },
                AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                RequirePkce = false,
                RedirectUris = { "http://localhost:3000/api/auth/callback/id-server" },
                AllowOfflineAccess = true,
                AllowedScopes = { "openid", "profile", "fashionPlace" },
                AccessTokenLifetime = 3600 * 24 * 30,
                AlwaysIncludeUserClaimsInIdToken = true
            },
            new Client
            {
                ClientId = "business",
                ClientName = "Business",
                AllowedScopes = { "openid", "profile", "fashionPlace" },
                ClientSecrets = { new Secret("secret-business".Sha256()) },
                AllowedGrantTypes = { GrantType.ResourceOwnerPassword }
            }
        ];
}
