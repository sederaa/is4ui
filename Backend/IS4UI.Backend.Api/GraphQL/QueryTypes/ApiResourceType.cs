using IS4UI.Backend.Data.Entities;
using HotChocolate.Types;
using IS4UI.Backend.Api.GraphQL.QueryResolvers;

namespace IS4UI.Backend.Api.GraphQL.QueryTypes
{
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
}

