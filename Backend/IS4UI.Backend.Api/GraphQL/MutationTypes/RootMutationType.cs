using System;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Types;
using IS4UI.Backend.Data;
using IS4UI.Backend.Data.Entities;

public class RootMutationType : ObjectType<Mutation>
{
    protected override void Configure(IObjectTypeDescriptor<Mutation> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Field(m => m.CreateClient(default, default))
            .Type<ClientType>()
            .Argument("input", a => a.Type<CreateClientInputType>());

        descriptor.Field(m => m.UpdateClient(default, default))
            .Type<ClientType>()
            .Argument("input", a => a.Type<UpdateClientInputType>());

        descriptor.Field(m => m.DeleteClient(default, default))
            .Type<ClientType>()
            .Argument("input", a => a.Type<DeleteClientInputType>());

    }
}

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

public class CreateClientInputType : InputObjectType<CreateClientInput>
{
    protected override void Configure(IInputObjectTypeDescriptor<CreateClientInput> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Field(x => x.ClientId)
            .Type<NonNullType<StringType>>();

        descriptor.Field(x => x.ClientName)
            .Type<NonNullType<StringType>>();
    }
}

public class UpdateClientInputType : InputObjectType<UpdateClientInput>
{
    protected override void Configure(IInputObjectTypeDescriptor<UpdateClientInput> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Field(x => x.Id)
            .Type<NonNullType<IntType>>();

        descriptor.Field(x => x.ClientId)
            .Type<NonNullType<StringType>>();

        descriptor.Field(x => x.ClientName)
            .Type<NonNullType<StringType>>();
    }
}

public class DeleteClientInputType : InputObjectType<DeleteClientInput>
{
    protected override void Configure(IInputObjectTypeDescriptor<DeleteClientInput> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Field(x => x.Id)
            .Type<NonNullType<IntType>>();
    }
}

public class CreateClientInput
{
    //public int Id { get; set; }        
    //public bool Enabled { get; set; }
    public string ClientId { get; set; }
    // public string ProtocolType { get; set; }
    // public bool RequireClientSecret { get; set; }
    public string ClientName { get; set; }
    // public string Description { get; set; }
    // public string ClientUri { get; set; }
    // public string LogoUri { get; set; }
    // public bool RequireConsent { get; set; }
    // public bool AllowRememberConsent { get; set; }
    // public bool AlwaysIncludeUserClaimsInIdToken { get; set; }
    // public bool RequirePkce { get; set; }
    // public bool AllowPlainTextPkce { get; set; }
    // public bool AllowAccessTokensViaBrowser { get; set; }
    // public string FrontChannelLogoutUri { get; set; }
    // public bool FrontChannelLogoutSessionRequired { get; set; }
    // public string BackChannelLogoutUri { get; set; }
    // public bool BackChannelLogoutSessionRequired { get; set; }
    // public bool AllowOfflineAccess { get; set; }
    // public int IdentityTokenLifetime { get; set; }
    // public string AllowedIdentityTokenSigningAlgorithms { get; set; }
    // public int AccessTokenLifetime { get; set; }
    // public int AuthorizationCodeLifetime { get; set; }
    // public int? ConsentLifetime { get; set; }
    // public int AbsoluteRefreshTokenLifetime { get; set; }
    // public int SlidingRefreshTokenLifetime { get; set; }
    // public int RefreshTokenUsage { get; set; }
    // public bool UpdateAccessTokenClaimsOnRefresh { get; set; }
    // public int RefreshTokenExpiration { get; set; }
    // public int AccessTokenType { get; set; }
    // public bool EnableLocalLogin { get; set; }
    // public bool IncludeJwtId { get; set; }
    // public bool AlwaysSendClientClaims { get; set; }
    // public string ClientClaimsPrefix { get; set; }
    // public string PairWiseSubjectSalt { get; set; }
    // public DateTime Created { get; set; }
    // public DateTime? Updated { get; set; }
    // public DateTime? LastAccessed { get; set; }
    // public int? UserSsoLifetime { get; set; }
    // public string UserCodeType { get; set; }
    // public int DeviceCodeLifetime { get; set; }
    // public bool NonEditable { get; set; }

}

public class UpdateClientInput
{
    public int Id { get; set; }
    //public bool Enabled { get; set; }
    public string ClientId { get; set; }
    // public string ProtocolType { get; set; }
    // public bool RequireClientSecret { get; set; }
    public string ClientName { get; set; }
    // public string Description { get; set; }
    // public string ClientUri { get; set; }
    // public string LogoUri { get; set; }
    // public bool RequireConsent { get; set; }
    // public bool AllowRememberConsent { get; set; }
    // public bool AlwaysIncludeUserClaimsInIdToken { get; set; }
    // public bool RequirePkce { get; set; }
    // public bool AllowPlainTextPkce { get; set; }
    // public bool AllowAccessTokensViaBrowser { get; set; }
    // public string FrontChannelLogoutUri { get; set; }
    // public bool FrontChannelLogoutSessionRequired { get; set; }
    // public string BackChannelLogoutUri { get; set; }
    // public bool BackChannelLogoutSessionRequired { get; set; }
    // public bool AllowOfflineAccess { get; set; }
    // public int IdentityTokenLifetime { get; set; }
    // public string AllowedIdentityTokenSigningAlgorithms { get; set; }
    // public int AccessTokenLifetime { get; set; }
    // public int AuthorizationCodeLifetime { get; set; }
    // public int? ConsentLifetime { get; set; }
    // public int AbsoluteRefreshTokenLifetime { get; set; }
    // public int SlidingRefreshTokenLifetime { get; set; }
    // public int RefreshTokenUsage { get; set; }
    // public bool UpdateAccessTokenClaimsOnRefresh { get; set; }
    // public int RefreshTokenExpiration { get; set; }
    // public int AccessTokenType { get; set; }
    // public bool EnableLocalLogin { get; set; }
    // public bool IncludeJwtId { get; set; }
    // public bool AlwaysSendClientClaims { get; set; }
    // public string ClientClaimsPrefix { get; set; }
    // public string PairWiseSubjectSalt { get; set; }
    // public DateTime Created { get; set; }
    // public DateTime? Updated { get; set; }
    // public DateTime? LastAccessed { get; set; }
    // public int? UserSsoLifetime { get; set; }
    // public string UserCodeType { get; set; }
    // public int DeviceCodeLifetime { get; set; }
    // public bool NonEditable { get; set; }

}

public class DeleteClientInput
{
    public int Id { get; set; }
}