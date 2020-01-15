using System;
using System.Collections.Generic;

namespace IS4UI.Backend.Data.Entities
{
    public partial class ApiScopeClaims
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int ApiScopeId { get; set; }

        public virtual ApiScopes ApiScope { get; set; }
    }
}
