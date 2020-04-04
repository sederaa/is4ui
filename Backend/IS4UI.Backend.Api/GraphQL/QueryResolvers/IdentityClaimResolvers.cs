using System.Collections.Generic;
using System.Linq;
using IS4UI.Backend.Data;
using IS4UI.Backend.Data.Entities;
using HotChocolate;

public class IdentityClaimResolvers
{
   public List<IdentityClaim> GetIdentityClaimsByParentId([Service] ApplicationDbContext db, [Parent]IdentityResource identityResource)
   {
       return db.IdentityClaims.Where(x => x.IdentityResourceId == identityResource.Id).ToList();
   }
}


