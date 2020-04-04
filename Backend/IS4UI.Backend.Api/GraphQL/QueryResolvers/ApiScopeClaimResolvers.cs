using System.Collections.Generic;
using System.Linq;
using IS4UI.Backend.Data;
using IS4UI.Backend.Data.Entities;
using HotChocolate;

public class ApiScopeClaimResolvers
{
   public List<ApiScopeClaim> GetApiScopeClaimsByParentId([Service] ApplicationDbContext db, [Parent]ApiScope apiScope)
   {
       return db.ApiScopeClaims.Where(x => x.ApiScopeId == apiScope.Id).ToList();
   }
}


