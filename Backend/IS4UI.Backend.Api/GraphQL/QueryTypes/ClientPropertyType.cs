using IS4UI.Backend.Data.Entities;
using HotChocolate.Types;

namespace IS4UI.Backend.Api.GraphQL.QueryTypes
{
    public class ClientPropertyType : ObjectType<ClientProperty>
    {
        protected override void Configure(IObjectTypeDescriptor<ClientProperty> descriptor)
        {
            descriptor.Field(x => x.Id)
                      .Type<NonNullType<IdType>>();

        }
    }
}

