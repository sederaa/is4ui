using System.Collections.Generic;
using System.Linq;
using IS4UI.Backend.Data;
using IS4UI.Backend.Data.Entities;
using HotChocolate;

public class ClientGrantTypeResolvers
{
   public List<ClientGrantType> GetClientGrantTypesByParentId([Service] ApplicationDbContext db, [Parent]Client client)
   {
       return db.ClientGrantTypes.Where(x => x.ClientId == client.Id).ToList();
   }
}


