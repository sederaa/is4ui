using IS4UI.Backend.Data.Entities;
using HotChocolate.Types;

namespace IS4UI.Backend.Api.GraphQL.QueryTypes
{
    public class ClientGrantTypeType : ObjectType<ClientGrantType>
    {
        protected override void Configure(IObjectTypeDescriptor<ClientGrantType> descriptor)
        {
            descriptor.Field(x => x.Id)
                      .Type<NonNullType<IdType>>();

        }
    }
}

