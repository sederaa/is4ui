using System;
using System.Threading.Tasks;
using HotChocolate;
using IS4UI.Backend.Data;
using IS4UI.Backend.Data.Entities;

public class Mutation
{
    public async Task<Client> CreateClient([Service] ApplicationDbContext db, CreateClientInput input)
    {
        var client = new Client
        {
            ClientId = input.ClientId,
            ClientName = input.ClientName,
            Created = DateTime.Now,
            Enabled = true,
            ProtocolType = "a",
            RequireClientSecret = false,
            RequireConsent = false,
            AllowRememberConsent = false,
            AlwaysIncludeUserClaimsInIdToken = false,
            RequirePkce = false,
            AllowPlainTextPkce = false,
            AllowAccessTokensViaBrowser = false,
            FrontChannelLogoutSessionRequired = false,
            BackChannelLogoutSessionRequired = false,
            AllowOfflineAccess = false,
            IdentityTokenLifetime = 1,
            AccessTokenLifetime = 1,
            AuthorizationCodeLifetime = 1,
            AbsoluteRefreshTokenLifetime = 1,
            SlidingRefreshTokenLifetime = 1,
            RefreshTokenUsage = 1,
            UpdateAccessTokenClaimsOnRefresh = false,
            RefreshTokenExpiration = 1,
            AccessTokenType = 1,
            EnableLocalLogin = false,
            IncludeJwtId = false,
            AlwaysSendClientClaims = false,
            DeviceCodeLifetime = 1,
            NonEditable = false
        };

        db.Clients.Add(client);
        await db.SaveChangesAsync();
        return client;
    }

    public async Task<Client> UpdateClient([Service] ApplicationDbContext db, UpdateClientInput input)
    {
        var client = await db.FindAsync<Client>(input.Id);
        client.ClientId = input.ClientId;
        client.ClientName = input.ClientName;
        await db.SaveChangesAsync();
        return client;
    }

    public async Task<Client> DeleteClient([Service] ApplicationDbContext db, DeleteClientInput input)
    {
        var client = await db.FindAsync<Client>(input.Id);
        db.Remove(client);
        await db.SaveChangesAsync();
        return client;
    }
}
