using System.Collections.Generic;
using System.Linq;
using IS4UI.Backend.Data;
using IS4UI.Backend.Data.Entities;

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
