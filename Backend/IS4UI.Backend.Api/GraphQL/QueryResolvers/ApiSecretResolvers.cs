using System.Collections.Generic;
using System.Linq;
using IS4UI.Backend.Data;
using IS4UI.Backend.Data.Entities;
using HotChocolate;

namespace IS4UI.Backend.Api.GraphQL.QueryResolvers
{
    public class ApiSecretResolvers
    {
        public List<ApiSecret> GetApiSecretsByParentId([Service] ApplicationDbContext db, [Parent]ApiResource apiResource)
        {
            return db.ApiSecrets.Where(x => x.ApiResourceId == apiResource.Id).ToList();
        }
    }
}

