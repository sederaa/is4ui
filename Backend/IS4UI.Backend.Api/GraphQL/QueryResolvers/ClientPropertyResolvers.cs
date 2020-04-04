using System.Collections.Generic;
using System.Linq;
using IS4UI.Backend.Data;
using IS4UI.Backend.Data.Entities;
using HotChocolate;

namespace IS4UI.Backend.Api.GraphQL.QueryResolvers
{
    public class ClientPropertyResolvers
    {
        public List<ClientProperty> GetClientPropertiesByParentId([Service] ApplicationDbContext db, [Parent]Client client)
        {
            return db.ClientProperties.Where(x => x.ClientId == client.Id).ToList();
        }
    }
}

