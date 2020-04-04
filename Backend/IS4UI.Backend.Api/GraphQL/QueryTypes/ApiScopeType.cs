using IS4UI.Backend.Data.Entities;
using HotChocolate.Types;
using IS4UI.Backend.Api.GraphQL.QueryResolvers;

namespace IS4UI.Backend.Api.GraphQL.QueryTypes
{
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
}

