using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using HotChocolate;
using HotChocolate.Types;
using IS4UI.Backend.Data;
using IS4UI.Backend.Data.Entities;

public class RootQueryType : ObjectType
{
    protected override void Configure(IObjectTypeDescriptor descriptor)
    {
        descriptor.Field<ClientResolvers>(x => x.GetClients()).Type<ListType<NonNullType<ClientType>>>();
        descriptor.Field<ClientResolvers>(x => x.GetClient(default)).Type<NonNullType<ClientType>>().Argument("id", a => a.Type<IntType>());
    }
}

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

public class ClientPostLogoutRedirectUriType : ObjectType<ClientPostLogoutRedirectUri>
{

}

public class ClientResolvers
{
    private readonly ApplicationDbContext db;

    public ClientResolvers(ApplicationDbContext db)
    {
        this.db = db;
    }

    public List<Client> GetClients()
    {
        return db.Clients.ToList();
    }

    public Client GetClient(int id)
    {
        return db.Clients.SingleOrDefault(c => c.Id == id);
    }

}


public class ClientPostLogoutRedirectUriResolvers
{

    private readonly ApplicationDbContext db;

    public ClientPostLogoutRedirectUriResolvers(ApplicationDbContext db)
    {
        this.db = db;
    }

    public List<ClientPostLogoutRedirectUri> GetClientPostLogoutRedirectUris()
    {
        return db.ClientPostLogoutRedirectUris.ToList();
    }

    public List<ClientPostLogoutRedirectUri> GetClientPostLogoutRedirectUrisByClientId(int clientId)
    {
        return db.ClientPostLogoutRedirectUris.Where(x => x.ClientId == clientId).ToList();
    }

    public List<ClientPostLogoutRedirectUri> GetClientPostLogoutRedirectUrisByClientIdFromParent([Parent]Client client)
    {
        return db.ClientPostLogoutRedirectUris.Where(x => x.ClientId == client.Id).ToList();
    }

    public ClientPostLogoutRedirectUri GetClientPostLogoutRedirectUri(int id)
    {
        return db.ClientPostLogoutRedirectUris.SingleOrDefault(x => x.Id == id);
    }

}