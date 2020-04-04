using System.Collections.Generic;
using System.Linq;
using IS4UI.Backend.Data;
using IS4UI.Backend.Data.Entities;
using HotChocolate;

public class ApiScopeResolvers
{
   public List<ApiScope> GetApiScopesByParentId([Service] ApplicationDbContext db, [Parent]ApiResource apiResource)
   {
       return db.ApiScopes.Where(x => x.ApiResourceId == apiResource.Id).ToList();
   }
}


