using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IS4UI.Backend.Data.Entities
{
    public partial class IdentityResource
    {
        public IdentityResource()
        {
            IdentityClaims = new HashSet<IdentityClaim>();
            IdentityProperties = new HashSet<IdentityProperty>();
        }

        [Key]
        public int Id { get; set; }
        public bool Enabled { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        [StringLength(200)]
        public string DisplayName { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }
        public bool Required { get; set; }
        public bool Emphasize { get; set; }
        public bool ShowInDiscoveryDocument { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public bool NonEditable { get; set; }

        [InverseProperty(nameof(IdentityClaim.IdentityResource))]
        public virtual ICollection<IdentityClaim> IdentityClaims { get; set; }
        [InverseProperty(nameof(IdentityProperty.IdentityResource))]
        public virtual ICollection<IdentityProperty> IdentityProperties { get; set; }
    }
}
