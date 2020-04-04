using IS4UI.Backend.Data.Entities;
using HotChocolate.Types;
using IS4UI.Backend.Api.GraphQL.QueryResolvers;

namespace IS4UI.Backend.Api.GraphQL.QueryTypes
{
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
}

