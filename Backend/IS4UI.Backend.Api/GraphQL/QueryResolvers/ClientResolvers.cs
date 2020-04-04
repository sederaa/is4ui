using System.Linq;
using IS4UI.Backend.Data;
using IS4UI.Backend.Data.Entities;
using HotChocolate;
using HotChocolate.Resolvers;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace IS4UI.Backend.Api.GraphQL.QueryResolvers
{
    public class ClientResolvers
    {
        public IQueryable<Client> GetClients([Service] ApplicationDbContext db)
        {
            return db.Clients;
        }

        public async Task<Client> GetClient([Service] ApplicationDbContext db, IResolverContext context, int id)
        {
            var dataLoader = context.BatchDataLoader<int, Client>(nameof(GetClient), async ids => await db.Clients.Where(c => ids.Contains(c.Id)).ToDictionaryAsync(x => x.Id, x => x));
            return await dataLoader.LoadAsync(id, context.RequestAborted);
        }
    }
}

