using IS4UI.Backend.Data.Entities;
using HotChocolate.Types;

namespace IS4UI.Backend.Api.GraphQL.QueryTypes
{
    public class ClientIdPrestrictionType : ObjectType<ClientIdPrestriction>
    {
        protected override void Configure(IObjectTypeDescriptor<ClientIdPrestriction> descriptor)
        {
            descriptor.Field(x => x.Id)
                      .Type<NonNullType<IdType>>();

        }
    }
}

