using System.Linq;
using IS4UI.Backend.Data;
using IS4UI.Backend.Data.Entities;
using HotChocolate;
using HotChocolate.Resolvers;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class PersistedGrantResolvers
{
   public IQueryable<PersistedGrant> GetPersistedGrants([Service] ApplicationDbContext db)
   {
      return db.PersistedGrants;
   }

   public async Task<PersistedGrant> GetPersistedGrant([Service] ApplicationDbContext db, IResolverContext context, string key)
   {
       var dataLoader = context.BatchDataLoader<string, PersistedGrant>(nameof(GetPersistedGrant), async keys => await db.PersistedGrants.Where(c => keys.Contains(c.Key)).ToDictionaryAsync(x => x.Key, x => x));
       return await dataLoader.LoadAsync(key, context.RequestAborted);
   }
}


