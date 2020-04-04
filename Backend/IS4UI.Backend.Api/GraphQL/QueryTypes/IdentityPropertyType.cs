using IS4UI.Backend.Data.Entities;
using HotChocolate.Types;

namespace IS4UI.Backend.Api.GraphQL.QueryTypes
{
    public class IdentityPropertyType : ObjectType<IdentityProperty>
    {
        protected override void Configure(IObjectTypeDescriptor<IdentityProperty> descriptor)
        {
            descriptor.Field(x => x.Id)
                      .Type<NonNullType<IdType>>();

        }
    }
}

