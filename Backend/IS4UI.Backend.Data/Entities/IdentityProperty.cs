using System;
using System.Collections.Generic;

namespace IS4UI.Backend.Data.Entities
{
    public partial class IdentityProperty
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public int IdentityResourceId { get; set; }

        public virtual IdentityResource IdentityResource { get; set; }
    }
}
