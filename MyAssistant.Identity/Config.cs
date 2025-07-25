﻿using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace MyAssistant.Identity;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource("roles",
                new[] {"role"})
        };

    public static IEnumerable<ApiResource> ApiResources =>
        new ApiResource[]
        {
            new ApiResource("MyAssistantAPI", "My Assistant API", new[] {"role"})
            {
                Scopes = { "MyAssistantAPI.full-access" },
            }
        };
    
    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("MyAssistantAPI.full-access"),
            new ApiScope("scope2"),
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            // m2m client credentials flow client
            new Client
            {
                ClientId = "m2m.client",
                ClientName = "Client Credentials Client",

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                AllowedScopes = { "scope1" }
            },

            // interactive client using code flow + pkce
            new Client
            {
                ClientId = "interactive",
                ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,

                RedirectUris = { "https://localhost:44300/signin-oidc" },
                FrontChannelLogoutUri = "https://localhost:44300/signout-oidc",
                PostLogoutRedirectUris = { "https://localhost:44300/signout-callback-oidc" },

                AllowOfflineAccess = true,
                AllowedScopes = { "openid", "profile", "scope2" }
            },
            new Client()
            {
                ClientName = "MyAssistant",
                ClientId = "myassistantclient",
                AllowedGrantTypes = GrantTypes.Code,
                AccessTokenType = AccessTokenType.Reference,
                AllowOfflineAccess = true,
                UpdateAccessTokenClaimsOnRefresh = true,
                RedirectUris = { "https://localhost:7046/signin-oidc" },  //TODO: Add the host address of the client (BLAZOR) here.
                       //{ "https://oauth.pstmn.io/v1/callback" }
                PostLogoutRedirectUris = {"https://localhost:7046/signout-callback-oidc"}, //TODO: Add the host address of the client (BLAZOR) here.
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "MyAssistantAPI.full-access",
                    "roles",
                },
                ClientSecrets =
                {
                    new Secret("secret".Sha256()),
                }
            } 
        };
}
