using IS4UI.Backend.Data.Entities;
using HotChocolate.Types;

namespace IS4UI.Backend.Api.GraphQL.QueryTypes
{
    public class IdentityClaimType : ObjectType<IdentityClaim>
    {
        protected override void Configure(IObjectTypeDescriptor<IdentityClaim> descriptor)
        {
            descriptor.Field(x => x.Id)
                      .Type<NonNullType<IdType>>();

        }
    }
}

