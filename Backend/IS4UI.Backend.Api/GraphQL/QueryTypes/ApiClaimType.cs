using IS4UI.Backend.Data.Entities;
using HotChocolate.Types;

namespace IS4UI.Backend.Api.GraphQL.QueryTypes
{
    public class ApiClaimType : ObjectType<ApiClaim>
    {
        protected override void Configure(IObjectTypeDescriptor<ApiClaim> descriptor)
        {
            descriptor.Field(x => x.Id)
                      .Type<NonNullType<IdType>>();

        }
    }
}

