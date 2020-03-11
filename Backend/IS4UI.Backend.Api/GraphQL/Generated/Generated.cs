using System.Collections.Generic;
using System.Linq;
using IS4UI.Backend.Data;
using IS4UI.Backend.Data.Entities;
using HotChocolate;
using HotChocolate.Resolvers;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using HotChocolate.Types;



 

////////////// QUERY TYPES //////////////
// Add to QueryTypes folder.

public class ApiClaimType : ObjectType<ApiClaim>
{
   protected override void Configure(IObjectTypeDescriptor<ApiClaim> descriptor)
   {
      descriptor.Field(x => x.Id)
                .Type<NonNullType<IdType>>();

   }
}

public class ApiPropertyType : ObjectType<ApiProperty>
{
   protected override void Configure(IObjectTypeDescriptor<ApiProperty> descriptor)
   {
      descriptor.Field(x => x.Id)
                .Type<NonNullType<IdType>>();

   }
}

public class ApiResourceType : ObjectType<ApiResource>
{
   protected override void Configure(IObjectTypeDescriptor<ApiResource> descriptor)
   {
      descriptor.Field<ApiClaimResolvers>(x => x.GetApiClaimsByParentId(default, default))
                .Type<ListType<NonNullType<ApiClaimType>>>()
                .Name("apiClaims");

      descriptor.Field<ApiPropertyResolvers>(x => x.GetApiPropertiesByParentId(default, default))
                .Type<ListType<NonNullType<ApiPropertyType>>>()
                .Name("apiProperties");

      descriptor.Field<ApiScopeResolvers>(x => x.GetApiScopesByParentId(default, default))
                .Type<ListType<NonNullType<ApiScopeType>>>()
                .Name("apiScopes");

      descriptor.Field<ApiSecretResolvers>(x => x.GetApiSecretsByParentId(default, default))
                .Type<ListType<NonNullType<ApiSecretType>>>()
                .Name("apiSecrets");

      descriptor.Field(x => x.Id)
                .Type<NonNullType<IdType>>();

   }
}

public class ApiScopeType : ObjectType<ApiScope>
{
   protected override void Configure(IObjectTypeDescriptor<ApiScope> descriptor)
   {
      descriptor.Field<ApiScopeClaimResolvers>(x => x.GetApiScopeClaimsByParentId(default, default))
                .Type<ListType<NonNullType<ApiScopeClaimType>>>()
                .Name("apiScopeClaims");

      descriptor.Field(x => x.Id)
                .Type<NonNullType<IdType>>();

   }
}

public class ApiScopeClaimType : ObjectType<ApiScopeClaim>
{
   protected override void Configure(IObjectTypeDescriptor<ApiScopeClaim> descriptor)
   {
      descriptor.Field(x => x.Id)
                .Type<NonNullType<IdType>>();

   }
}

public class ApiSecretType : ObjectType<ApiSecret>
{
   protected override void Configure(IObjectTypeDescriptor<ApiSecret> descriptor)
   {
      descriptor.Field(x => x.Id)
                .Type<NonNullType<IdType>>();

   }
}

public class ClientType : ObjectType<Client>
{
   protected override void Configure(IObjectTypeDescriptor<Client> descriptor)
   {
      descriptor.Field<ClientClaimResolvers>(x => x.GetClientClaimsByParentId(default, default))
                .Type<ListType<NonNullType<ClientClaimType>>>()
                .Name("clientClaims");

      descriptor.Field<ClientCorsOriginResolvers>(x => x.GetClientCorsOriginsByParentId(default, default))
                .Type<ListType<NonNullType<ClientCorsOriginType>>>()
                .Name("clientCorsOrigins");

      descriptor.Field<ClientGrantTypeResolvers>(x => x.GetClientGrantTypesByParentId(default, default))
                .Type<ListType<NonNullType<ClientGrantTypeType>>>()
                .Name("clientGrantTypes");

      descriptor.Field<ClientIdPrestrictionResolvers>(x => x.GetClientIdPrestrictionsByParentId(default, default))
                .Type<ListType<NonNullType<ClientIdPrestrictionType>>>()
                .Name("clientIdPrestrictions");

      descriptor.Field<ClientPostLogoutRedirectUriResolvers>(x => x.GetClientPostLogoutRedirectUrisByParentId(default, default))
                .Type<ListType<NonNullType<ClientPostLogoutRedirectUriType>>>()
                .Name("clientPostLogoutRedirectUris");

      descriptor.Field<ClientPropertyResolvers>(x => x.GetClientPropertiesByParentId(default, default))
                .Type<ListType<NonNullType<ClientPropertyType>>>()
                .Name("clientProperties");

      descriptor.Field<ClientRedirectUriResolvers>(x => x.GetClientRedirectUrisByParentId(default, default))
                .Type<ListType<NonNullType<ClientRedirectUriType>>>()
                .Name("clientRedirectUris");

      descriptor.Field<ClientScopeResolvers>(x => x.GetClientScopesByParentId(default, default))
                .Type<ListType<NonNullType<ClientScopeType>>>()
                .Name("clientScopes");

      descriptor.Field<ClientSecretResolvers>(x => x.GetClientSecretsByParentId(default, default))
                .Type<ListType<NonNullType<ClientSecretType>>>()
                .Name("clientSecrets");

      descriptor.Field(x => x.Id)
                .Type<NonNullType<IdType>>();

   }
}

public class ClientClaimType : ObjectType<ClientClaim>
{
   protected override void Configure(IObjectTypeDescriptor<ClientClaim> descriptor)
   {
      descriptor.Field(x => x.Id)
                .Type<NonNullType<IdType>>();

   }
}

public class ClientCorsOriginType : ObjectType<ClientCorsOrigin>
{
   protected override void Configure(IObjectTypeDescriptor<ClientCorsOrigin> descriptor)
   {
      descriptor.Field(x => x.Id)
                .Type<NonNullType<IdType>>();

   }
}

public class ClientGrantTypeType : ObjectType<ClientGrantType>
{
   protected override void Configure(IObjectTypeDescriptor<ClientGrantType> descriptor)
   {
      descriptor.Field(x => x.Id)
                .Type<NonNullType<IdType>>();

   }
}

public class ClientIdPrestrictionType : ObjectType<ClientIdPrestriction>
{
   protected override void Configure(IObjectTypeDescriptor<ClientIdPrestriction> descriptor)
   {
      descriptor.Field(x => x.Id)
                .Type<NonNullType<IdType>>();

   }
}

public class ClientPostLogoutRedirectUriType : ObjectType<ClientPostLogoutRedirectUri>
{
   protected override void Configure(IObjectTypeDescriptor<ClientPostLogoutRedirectUri> descriptor)
   {
      descriptor.Field(x => x.Id)
                .Type<NonNullType<IdType>>();

   }
}

public class ClientPropertyType : ObjectType<ClientProperty>
{
   protected override void Configure(IObjectTypeDescriptor<ClientProperty> descriptor)
   {
      descriptor.Field(x => x.Id)
                .Type<NonNullType<IdType>>();

   }
}

public class ClientRedirectUriType : ObjectType<ClientRedirectUri>
{
   protected override void Configure(IObjectTypeDescriptor<ClientRedirectUri> descriptor)
   {
      descriptor.Field(x => x.Id)
                .Type<NonNullType<IdType>>();

   }
}

public class ClientScopeType : ObjectType<ClientScope>
{
   protected override void Configure(IObjectTypeDescriptor<ClientScope> descriptor)
   {
      descriptor.Field(x => x.Id)
                .Type<NonNullType<IdType>>();

   }
}

public class ClientSecretType : ObjectType<ClientSecret>
{
   protected override void Configure(IObjectTypeDescriptor<ClientSecret> descriptor)
   {
      descriptor.Field(x => x.Id)
                .Type<NonNullType<IdType>>();

   }
}

public class DeviceCodeType : ObjectType<DeviceCode>
{
   protected override void Configure(IObjectTypeDescriptor<DeviceCode> descriptor)
   {
      descriptor.Field(x => x.UserCode)
                .Type<NonNullType<IdType>>();

   }
}

public class IdentityClaimType : ObjectType<IdentityClaim>
{
   protected override void Configure(IObjectTypeDescriptor<IdentityClaim> descriptor)
   {
      descriptor.Field(x => x.Id)
                .Type<NonNullType<IdType>>();

   }
}

public class IdentityPropertyType : ObjectType<IdentityProperty>
{
   protected override void Configure(IObjectTypeDescriptor<IdentityProperty> descriptor)
   {
      descriptor.Field(x => x.Id)
                .Type<NonNullType<IdType>>();

   }
}

public class IdentityResourceType : ObjectType<IdentityResource>
{
   protected override void Configure(IObjectTypeDescriptor<IdentityResource> descriptor)
   {
      descriptor.Field(x => x.Id)
                .Type<NonNullType<IdType>>();

      descriptor.Field<IdentityClaimResolvers>(x => x.GetIdentityClaimsByParentId(default, default))
                .Type<ListType<NonNullType<IdentityClaimType>>>()
                .Name("identityClaims");

      descriptor.Field<IdentityPropertyResolvers>(x => x.GetIdentityPropertiesByParentId(default, default))
                .Type<ListType<NonNullType<IdentityPropertyType>>>()
                .Name("identityProperties");

   }
}

public class PersistedGrantType : ObjectType<PersistedGrant>
{
   protected override void Configure(IObjectTypeDescriptor<PersistedGrant> descriptor)
   {
      descriptor.Field(x => x.Key)
                .Type<NonNullType<IdType>>();

   }
}

////////////// RESOLVERS //////////////
// Add to Resolvers folder.

public class ApiClaimResolvers
{
   public List<ApiClaim> GetApiClaimsByParentId([Service] ApplicationDbContext db, [Parent]ApiResource apiResource)
   {
       return db.ApiClaims.Where(x => x.ApiResourceId == apiResource.Id).ToList();
   }
}

public class ApiPropertyResolvers
{
   public List<ApiProperty> GetApiPropertiesByParentId([Service] ApplicationDbContext db, [Parent]ApiResource apiResource)
   {
       return db.ApiProperties.Where(x => x.ApiResourceId == apiResource.Id).ToList();
   }
}

public class ApiResourceResolvers
{
   public IQueryable<ApiResource> GetApiResources([Service] ApplicationDbContext db)
   {
      return db.ApiResources;
   }

   public async Task<ApiResource> GetApiResource([Service] ApplicationDbContext db, IResolverContext context, int id)
   {
       var dataLoader = context.BatchDataLoader<int, ApiResource>(nameof(GetApiResource), async ids => await db.ApiResources.Where(c => ids.Contains(c.Id)).ToDictionaryAsync(x => x.Id, x => x));
       return await dataLoader.LoadAsync(id, context.RequestAborted);
   }
}

public class ApiScopeResolvers
{
   public List<ApiScope> GetApiScopesByParentId([Service] ApplicationDbContext db, [Parent]ApiResource apiResource)
   {
       return db.ApiScopes.Where(x => x.ApiResourceId == apiResource.Id).ToList();
   }
}

public class ApiScopeClaimResolvers
{
   public List<ApiScopeClaim> GetApiScopeClaimsByParentId([Service] ApplicationDbContext db, [Parent]ApiScope apiScope)
   {
       return db.ApiScopeClaims.Where(x => x.ApiScopeId == apiScope.Id).ToList();
   }
}

public class ApiSecretResolvers
{
   public List<ApiSecret> GetApiSecretsByParentId([Service] ApplicationDbContext db, [Parent]ApiResource apiResource)
   {
       return db.ApiSecrets.Where(x => x.ApiResourceId == apiResource.Id).ToList();
   }
}

public class ClientResolvers
{
   public IQueryable<Client> GetClients([Service] ApplicationDbContext db)
   {
      return db.Clients;
   }

   public async Task<Client> GetClient([Service] ApplicationDbContext db, IResolverContext context, int id)
   {
       var dataLoader = context.BatchDataLoader<int, Client>(nameof(GetClient), async ids => await db.Clients.Where(c => ids.Contains(c.Id)).ToDictionaryAsync(x => x.Id, x => x));
       return await dataLoader.LoadAsync(id, context.RequestAborted);
   }
}

public class ClientClaimResolvers
{
   public List<ClientClaim> GetClientClaimsByParentId([Service] ApplicationDbContext db, [Parent]Client client)
   {
       return db.ClientClaims.Where(x => x.ClientId == client.Id).ToList();
   }
}

public class ClientCorsOriginResolvers
{
   public List<ClientCorsOrigin> GetClientCorsOriginsByParentId([Service] ApplicationDbContext db, [Parent]Client client)
   {
       return db.ClientCorsOrigins.Where(x => x.ClientId == client.Id).ToList();
   }
}

public class ClientGrantTypeResolvers
{
   public List<ClientGrantType> GetClientGrantTypesByParentId([Service] ApplicationDbContext db, [Parent]Client client)
   {
       return db.ClientGrantTypes.Where(x => x.ClientId == client.Id).ToList();
   }
}

public class ClientIdPrestrictionResolvers
{
   public List<ClientIdPrestriction> GetClientIdPrestrictionsByParentId([Service] ApplicationDbContext db, [Parent]Client client)
   {
       return db.ClientIdPrestrictions.Where(x => x.ClientId == client.Id).ToList();
   }
}

public class ClientPostLogoutRedirectUriResolvers
{
   public List<ClientPostLogoutRedirectUri> GetClientPostLogoutRedirectUrisByParentId([Service] ApplicationDbContext db, [Parent]Client client)
   {
       return db.ClientPostLogoutRedirectUris.Where(x => x.ClientId == client.Id).ToList();
   }
}

public class ClientPropertyResolvers
{
   public List<ClientProperty> GetClientPropertiesByParentId([Service] ApplicationDbContext db, [Parent]Client client)
   {
       return db.ClientProperties.Where(x => x.ClientId == client.Id).ToList();
   }
}

public class ClientRedirectUriResolvers
{
   public List<ClientRedirectUri> GetClientRedirectUrisByParentId([Service] ApplicationDbContext db, [Parent]Client client)
   {
       return db.ClientRedirectUris.Where(x => x.ClientId == client.Id).ToList();
   }
}

public class ClientScopeResolvers
{
   public List<ClientScope> GetClientScopesByParentId([Service] ApplicationDbContext db, [Parent]Client client)
   {
       return db.ClientScopes.Where(x => x.ClientId == client.Id).ToList();
   }
}

public class ClientSecretResolvers
{
   public List<ClientSecret> GetClientSecretsByParentId([Service] ApplicationDbContext db, [Parent]Client client)
   {
       return db.ClientSecrets.Where(x => x.ClientId == client.Id).ToList();
   }
}

public class DeviceCodeResolvers
{
   public IQueryable<DeviceCode> GetDeviceCodes([Service] ApplicationDbContext db)
   {
      return db.DeviceCodes;
   }

   public async Task<DeviceCode> GetDeviceCode([Service] ApplicationDbContext db, IResolverContext context, int id)
   {
       var dataLoader = context.BatchDataLoader<int, DeviceCode>(nameof(GetDeviceCode), async ids => await db.DeviceCodes.Where(c => ids.Contains(c.Id)).ToDictionaryAsync(x => x.Id, x => x));
       return await dataLoader.LoadAsync(id, context.RequestAborted);
   }
}

public class IdentityClaimResolvers
{
   public List<IdentityClaim> GetIdentityClaimsByParentId([Service] ApplicationDbContext db, [Parent]IdentityResource identityResource)
   {
       return db.IdentityClaims.Where(x => x.IdentityResourceId == identityResource.Id).ToList();
   }
}

public class IdentityPropertyResolvers
{
   public List<IdentityProperty> GetIdentityPropertiesByParentId([Service] ApplicationDbContext db, [Parent]IdentityResource identityResource)
   {
       return db.IdentityProperties.Where(x => x.IdentityResourceId == identityResource.Id).ToList();
   }
}

public class IdentityResourceResolvers
{
   public IQueryable<IdentityResource> GetIdentityResources([Service] ApplicationDbContext db)
   {
      return db.IdentityResources;
   }

   public async Task<IdentityResource> GetIdentityResource([Service] ApplicationDbContext db, IResolverContext context, int id)
   {
       var dataLoader = context.BatchDataLoader<int, IdentityResource>(nameof(GetIdentityResource), async ids => await db.IdentityResources.Where(c => ids.Contains(c.Id)).ToDictionaryAsync(x => x.Id, x => x));
       return await dataLoader.LoadAsync(id, context.RequestAborted);
   }
}

public class PersistedGrantResolvers
{
   public IQueryable<PersistedGrant> GetPersistedGrants([Service] ApplicationDbContext db)
   {
      return db.PersistedGrants;
   }

   public async Task<PersistedGrant> GetPersistedGrant([Service] ApplicationDbContext db, IResolverContext context, int id)
   {
       var dataLoader = context.BatchDataLoader<int, PersistedGrant>(nameof(GetPersistedGrant), async ids => await db.PersistedGrants.Where(c => ids.Contains(c.Id)).ToDictionaryAsync(x => x.Id, x => x));
       return await dataLoader.LoadAsync(id, context.RequestAborted);
   }
}

////////////// ROOT QUERY TYPES //////////////
// Add to RootQueryType class.

public class RootQueryType : ObjectType
{
    protected override void Configure(IObjectTypeDescriptor descriptor)
    {
      descriptor.Field<ApiResourceResolvers>(x => x.GetApiResources(default))
                .Type<ListType<NonNullType<ApiResourceType>>>()
                .UseFiltering();

      descriptor.Field<ApiResourceResolvers>(x => x.GetApiResource(default,default, default))
                .Type<ApiResourceType>()
                .Argument("id", a => a.Type<IntType>());

      descriptor.Field<ClientResolvers>(x => x.GetClients(default))
                .Type<ListType<NonNullType<ClientType>>>()
                .UseFiltering();

      descriptor.Field<ClientResolvers>(x => x.GetClient(default,default, default))
                .Type<ClientType>()
                .Argument("id", a => a.Type<IntType>());

      descriptor.Field<DeviceCodeResolvers>(x => x.GetDeviceCodes(default))
                .Type<ListType<NonNullType<DeviceCodeType>>>()
                .UseFiltering();

      descriptor.Field<DeviceCodeResolvers>(x => x.GetDeviceCode(default,default, default))
                .Type<DeviceCodeType>()
                .Argument("id", a => a.Type<IntType>());

      descriptor.Field<IdentityResourceResolvers>(x => x.GetIdentityResources(default))
                .Type<ListType<NonNullType<IdentityResourceType>>>()
                .UseFiltering();

      descriptor.Field<IdentityResourceResolvers>(x => x.GetIdentityResource(default,default, default))
                .Type<IdentityResourceType>()
                .Argument("id", a => a.Type<IntType>());

      descriptor.Field<PersistedGrantResolvers>(x => x.GetPersistedGrants(default))
                .Type<ListType<NonNullType<PersistedGrantType>>>()
                .UseFiltering();

      descriptor.Field<PersistedGrantResolvers>(x => x.GetPersistedGrant(default,default, default))
                .Type<PersistedGrantType>()
                .Argument("id", a => a.Type<IntType>());

   }
}


