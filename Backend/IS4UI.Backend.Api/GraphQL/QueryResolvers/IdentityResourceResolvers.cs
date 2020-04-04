using System.Linq;
using IS4UI.Backend.Data;
using IS4UI.Backend.Data.Entities;
using HotChocolate;
using HotChocolate.Resolvers;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class IdentityResourceResolvers
{
   public IQueryable<IdentityResource> GetIdentityResources([Service] ApplicationDbContext db)
   {
      return db.IdentityResources;
   }

   public async Task<IdentityResource> GetIdentityResource([Service] ApplicationDbContext db, IResolverContext context, int id)
   {
       var dataLoader = context.BatchDataLoader<int, IdentityResource>(nameof(GetIdentityResource), async ids => await db.IdentityResources.Where(c => ids.Contains(c.Id)).ToDictionaryAsync(x => x.Id, x => x));
       return await dataLoader.LoadAsync(id, context.RequestAborted);
   }
}


