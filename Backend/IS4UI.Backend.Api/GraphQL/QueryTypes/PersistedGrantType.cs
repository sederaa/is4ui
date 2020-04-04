using IS4UI.Backend.Data.Entities;
using HotChocolate.Types;

namespace IS4UI.Backend.Api.GraphQL.QueryTypes
{
    public class PersistedGrantType : ObjectType<PersistedGrant>
    {
        protected override void Configure(IObjectTypeDescriptor<PersistedGrant> descriptor)
        {
            descriptor.Field(x => x.Key)
                      .Type<NonNullType<IdType>>();

        }
    }
}

