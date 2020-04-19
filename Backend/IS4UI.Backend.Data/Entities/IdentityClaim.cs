using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IS4UI.Backend.Data.Entities
{
    public partial class IdentityClaim
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Type { get; set; }
        public int IdentityResourceId { get; set; }

        [ForeignKey(nameof(IdentityResourceId))]
        [InverseProperty("IdentityClaims")]
        public virtual IdentityResource IdentityResource { get; set; }
    }
}
