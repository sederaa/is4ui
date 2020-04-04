using System.Collections.Generic;
using System.Linq;
using IS4UI.Backend.Data;
using IS4UI.Backend.Data.Entities;
using HotChocolate;

public class ClientIdPrestrictionResolvers
{
   public List<ClientIdPrestriction> GetClientIdPrestrictionsByParentId([Service] ApplicationDbContext db, [Parent]Client client)
   {
       return db.ClientIdPrestrictions.Where(x => x.ClientId == client.Id).ToList();
   }
}


