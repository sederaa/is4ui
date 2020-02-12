using System.Collections.Generic;
using System.Linq;
using IS4UI.Backend.Data;
using IS4UI.Backend.Data.Entities;
using HotChocolate;

public class ClientResolvers
{
    public List<Client> GetClients([Service] ApplicationDbContext db)
    {
        return db.Clients.ToList();
    }

    public Client GetClient([Service] ApplicationDbContext db, int id)
    {
        return db.Clients.SingleOrDefault(c => c.Id == id);
    }

}
