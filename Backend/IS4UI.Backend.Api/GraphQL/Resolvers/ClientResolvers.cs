using System.Collections.Generic;
using System.Linq;
using IS4UI.Backend.Data;
using IS4UI.Backend.Data.Entities;
using HotChocolate;
using HotChocolate.Resolvers;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class ClientResolvers
{
    public IQueryable<Client> GetClients([Service] ApplicationDbContext db)
    {
        return db.Clients;
    }

    public async Task<Client> GetClient([Service] ApplicationDbContext db, IResolverContext context, int id)
    {
        var dataLoader = context.BatchDataLoader<int, Client>(nameof(GetClient), async clientIds => await db.Clients.Where(c => clientIds.Contains(c.Id)).ToDictionaryAsync(x => x.Id, x => x));
        return await dataLoader.LoadAsync(id, context.RequestAborted);
    }

}
