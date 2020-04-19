using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IS4UI.Backend.Data.Entities
{
    public partial class ApiScopeClaim
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Type { get; set; }
        public int ApiScopeId { get; set; }

        [ForeignKey(nameof(ApiScopeId))]
        [InverseProperty("ApiScopeClaims")]
        public virtual ApiScope ApiScope { get; set; }
    }
}
