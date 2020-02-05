using System;
using System.Collections.Generic;

namespace IS4UI.Backend.Data.Entities
{
    public partial class IdentityClaim
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int IdentityResourceId { get; set; }

        public virtual IdentityResource IdentityResource { get; set; }
    }
}
