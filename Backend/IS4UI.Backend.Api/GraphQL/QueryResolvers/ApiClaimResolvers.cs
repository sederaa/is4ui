using System.Collections.Generic;
using System.Linq;
using IS4UI.Backend.Data;
using IS4UI.Backend.Data.Entities;
using HotChocolate;
////////////// RESOLVERS //////////////
// Add to Resolvers folder.

public class ApiClaimResolvers
{
   public List<ApiClaim> GetApiClaimsByParentId([Service] ApplicationDbContext db, [Parent]ApiResource apiResource)
   {
       return db.ApiClaims.Where(x => x.ApiResourceId == apiResource.Id).ToList();
   }
}


