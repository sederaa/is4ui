using System.Collections.Generic;
using System.Linq;
using IS4UI.Backend.Data;
using IS4UI.Backend.Data.Entities;
using HotChocolate;

public class IdentityPropertyResolvers
{
   public List<IdentityProperty> GetIdentityPropertiesByParentId([Service] ApplicationDbContext db, [Parent]IdentityResource identityResource)
   {
       return db.IdentityProperties.Where(x => x.IdentityResourceId == identityResource.Id).ToList();
   }
}


