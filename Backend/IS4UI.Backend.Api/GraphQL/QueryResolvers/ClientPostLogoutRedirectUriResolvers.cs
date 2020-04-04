using System.Collections.Generic;
using System.Linq;
using IS4UI.Backend.Data;
using IS4UI.Backend.Data.Entities;
using HotChocolate;

namespace IS4UI.Backend.Api.GraphQL.QueryResolvers
{
    public class ClientPostLogoutRedirectUriResolvers
    {
        public List<ClientPostLogoutRedirectUri> GetClientPostLogoutRedirectUrisByParentId([Service] ApplicationDbContext db, [Parent]Client client)
        {
            return db.ClientPostLogoutRedirectUris.Where(x => x.ClientId == client.Id).ToList();
        }
    }
}

