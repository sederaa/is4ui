﻿using System;
using System.Collections.Generic;

namespace IS4UI.Backend.Data.Entities
{
    public partial class IdentityClaims
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int IdentityResourceId { get; set; }

        public virtual IdentityResources IdentityResource { get; set; }
    }
}