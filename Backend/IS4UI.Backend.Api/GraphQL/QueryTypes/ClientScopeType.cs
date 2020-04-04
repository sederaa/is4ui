using IS4UI.Backend.Data.Entities;
using HotChocolate.Types;

namespace IS4UI.Backend.Api.GraphQL.QueryTypes
{
    public class ClientScopeType : ObjectType<ClientScope>
    {
        protected override void Configure(IObjectTypeDescriptor<ClientScope> descriptor)
        {
            descriptor.Field(x => x.Id)
                      .Type<NonNullType<IdType>>();

        }
    }
}

