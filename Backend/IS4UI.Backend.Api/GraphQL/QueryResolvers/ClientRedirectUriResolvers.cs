using System.Collections.Generic;
using System.Linq;
using IS4UI.Backend.Data;
using IS4UI.Backend.Data.Entities;
using HotChocolate;

namespace IS4UI.Backend.Api.GraphQL.QueryResolvers
{
    public class ClientRedirectUriResolvers
    {
        public List<ClientRedirectUri> GetClientRedirectUrisByParentId([Service] ApplicationDbContext db, [Parent]Client client)
        {
            return db.ClientRedirectUris.Where(x => x.ClientId == client.Id).ToList();
        }
    }
}

