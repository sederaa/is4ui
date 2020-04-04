using System.Collections.Generic;
using System.Linq;
using IS4UI.Backend.Data;
using IS4UI.Backend.Data.Entities;
using HotChocolate;

public class ClientScopeResolvers
{
   public List<ClientScope> GetClientScopesByParentId([Service] ApplicationDbContext db, [Parent]Client client)
   {
       return db.ClientScopes.Where(x => x.ClientId == client.Id).ToList();
   }
}


