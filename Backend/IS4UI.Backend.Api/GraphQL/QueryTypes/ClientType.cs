using HotChocolate.Types;
using IS4UI.Backend.Data.Entities;

public class ClientType : ObjectType<Client>
{
    protected override void Configure(IObjectTypeDescriptor<Client> descriptor)
    {
        descriptor.Field(x=>x.Id)
                  .Type<NonNullType<IdType>>();
                  
        descriptor.Field<ClientPostLogoutRedirectUriResolvers>(x => x.GetClientPostLogoutRedirectUrisByClientIdFromParent(default))
                  .Type<ListType<NonNullType<ClientPostLogoutRedirectUriType>>>()
                  .Name("clientPostLogoutRedirectUris");
    }
}
