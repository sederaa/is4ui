using HotChocolate.Types;
using IS4UI.Backend.Api.GraphQL.QueryResolvers;

namespace IS4UI.Backend.Api.GraphQL.QueryTypes
{
    public class RootQueryType : ObjectType
    {
        protected override void Configure(IObjectTypeDescriptor descriptor)
        {
            descriptor.Field<ApiResourceResolvers>(x => x.GetApiResources(default))
                      .Type<ListType<NonNullType<ApiResourceType>>>()
                      .UseFiltering();

            descriptor.Field<ApiResourceResolvers>(x => x.GetApiResource(default, default, default))
                      .Type<ApiResourceType>()
                      .Argument("id", a => a.Type<IntType>());

            descriptor.Field<ClientResolvers>(x => x.GetClients(default))
                      .Type<ListType<NonNullType<ClientType>>>()
                      .UseFiltering();

            descriptor.Field<ClientResolvers>(x => x.GetClient(default, default, default))
                      .Type<ClientType>()
                      .Argument("id", a => a.Type<IntType>());

            descriptor.Field<DeviceCodeResolvers>(x => x.GetDeviceCodes(default))
                      .Type<ListType<NonNullType<DeviceCodeType>>>()
                      .UseFiltering();

            descriptor.Field<DeviceCodeResolvers>(x => x.GetDeviceCode(default, default, default))
                      .Type<DeviceCodeType>()
                      .Argument("id", a => a.Type<IntType>());

            descriptor.Field<IdentityResourceResolvers>(x => x.GetIdentityResources(default))
                      .Type<ListType<NonNullType<IdentityResourceType>>>()
                      .UseFiltering();

            descriptor.Field<IdentityResourceResolvers>(x => x.GetIdentityResource(default, default, default))
                      .Type<IdentityResourceType>()
                      .Argument("id", a => a.Type<IntType>());

            descriptor.Field<PersistedGrantResolvers>(x => x.GetPersistedGrants(default))
                      .Type<ListType<NonNullType<PersistedGrantType>>>()
                      .UseFiltering();

            descriptor.Field<PersistedGrantResolvers>(x => x.GetPersistedGrant(default, default, default))
                      .Type<PersistedGrantType>()
                      .Argument("id", a => a.Type<IntType>());

        }
    }
}