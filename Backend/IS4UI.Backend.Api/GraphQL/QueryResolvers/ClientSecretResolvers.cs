using System.Collections.Generic;
using System.Linq;
using IS4UI.Backend.Data;
using IS4UI.Backend.Data.Entities;
using HotChocolate;

public class ClientSecretResolvers
{
   public List<ClientSecret> GetClientSecretsByParentId([Service] ApplicationDbContext db, [Parent]Client client)
   {
       return db.ClientSecrets.Where(x => x.ClientId == client.Id).ToList();
   }
}


