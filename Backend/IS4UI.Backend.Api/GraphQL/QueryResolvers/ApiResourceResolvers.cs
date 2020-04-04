using System.Linq;
using IS4UI.Backend.Data;
using IS4UI.Backend.Data.Entities;
using HotChocolate;
using HotChocolate.Resolvers;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace IS4UI.Backend.Api.GraphQL.QueryResolvers
{
    public class ApiResourceResolvers
    {
        public IQueryable<ApiResource> GetApiResources([Service] ApplicationDbContext db)
        {
            return db.ApiResources;
        }

        public async Task<ApiResource> GetApiResource([Service] ApplicationDbContext db, IResolverContext context, int id)
        {
            var dataLoader = context.BatchDataLoader<int, ApiResource>(nameof(GetApiResource), async ids => await db.ApiResources.Where(c => ids.Contains(c.Id)).ToDictionaryAsync(x => x.Id, x => x));
            return await dataLoader.LoadAsync(id, context.RequestAborted);
        }
    }
}

