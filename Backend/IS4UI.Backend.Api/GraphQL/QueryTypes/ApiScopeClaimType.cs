using IS4UI.Backend.Data.Entities;
using HotChocolate.Types;

namespace IS4UI.Backend.Api.GraphQL.QueryTypes
{
    public class ApiScopeClaimType : ObjectType<ApiScopeClaim>
    {
        protected override void Configure(IObjectTypeDescriptor<ApiScopeClaim> descriptor)
        {
            descriptor.Field(x => x.Id)
                      .Type<NonNullType<IdType>>();

        }
    }
}

