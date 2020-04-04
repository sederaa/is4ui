using System.Collections.Generic;
using System.Linq;
using IS4UI.Backend.Data;
using IS4UI.Backend.Data.Entities;
using HotChocolate;

namespace IS4UI.Backend.Api.GraphQL.QueryResolvers
{
    public class ApiPropertyResolvers
    {
        public List<ApiProperty> GetApiPropertiesByParentId([Service] ApplicationDbContext db, [Parent]ApiResource apiResource)
        {
            return db.ApiProperties.Where(x => x.ApiResourceId == apiResource.Id).ToList();
        }
    }
}