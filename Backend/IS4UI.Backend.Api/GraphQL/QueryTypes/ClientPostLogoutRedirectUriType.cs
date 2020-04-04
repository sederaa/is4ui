using IS4UI.Backend.Data.Entities;
using HotChocolate.Types;

namespace IS4UI.Backend.Api.GraphQL.QueryTypes
{
    public class ClientPostLogoutRedirectUriType : ObjectType<ClientPostLogoutRedirectUri>
    {
        protected override void Configure(IObjectTypeDescriptor<ClientPostLogoutRedirectUri> descriptor)
        {
            descriptor.Field(x => x.Id)
                      .Type<NonNullType<IdType>>();

        }
    }
}

