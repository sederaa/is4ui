using System.Collections.Generic;
using System.Linq;
using HotChocolate;
using HotChocolate.Types;
using IS4UI.Backend.Data;
using IS4UI.Backend.Data.Entities;

public class RootQueryType : ObjectType
{
    protected override void Configure(IObjectTypeDescriptor descriptor)
    {
        descriptor.Field<ClientsResolvers>(x => x.GetClients()).Type<ListType<NonNullType<ClientType>>>();
    }
}

public class ClientType : ObjectType<Clients>
{

}

public class ClientsResolvers
{
    private readonly IClientsRepo repo;
    private readonly ApplicationDbContext db;

    public ClientsResolvers(IClientsRepo repo, ApplicationDbContext db)
    {
        this.repo = repo;
        this.db = db;
    }

    public List<Clients> GetClients()
    {
        return db.Clients.ToList();
        //return repo.GetClients();
    }
}

public interface IClientsRepo
{
    List<Clients> GetClients();
}
public class ClientsRepo : IClientsRepo
{
    public List<Clients> GetClients()
    {
        return new List<Clients>{
            new Clients{ClientId="a"}
        };
    }
}