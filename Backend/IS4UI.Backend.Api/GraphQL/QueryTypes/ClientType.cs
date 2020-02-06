using HotChocolate.Types;
using IS4UI.Backend.Data.Entities;

public class ClientType : ObjectType<Client>
{
    public ClientType()
    {

    }

    protected override void Configure(IObjectTypeDescriptor<Client> descriptor)
    {
        descriptor.Field<ClientPostLogoutRedirectUriResolvers>(x => x.GetClientPostLogoutRedirectUrisByClientIdFromParent(default))
                  .Type<ListType<NonNullType<ClientPostLogoutRedirectUriType>>>()
                  .Name("clientPostLogoutRedirectUris");

        // Another approach without magic string:
        // descriptor.Field(x => x.ClientPostLogoutRedirectUris)
        //           .Type<ListType<NonNullType<ClientPostLogoutRedirectUriType>>>()
        //           .Resolver(context =>
        //          {
        //              var resolver = context.Resolver<ClientPostLogoutRedirectUriResolvers>();
        //              var clientId = context.Parent<Client>().Id;
        //              var result = resolver.GetClientPostLogoutRedirectUrisByClientId(clientId);
        //              return result;
        //          });

    }
}
