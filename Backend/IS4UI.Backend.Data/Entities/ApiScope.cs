using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IS4UI.Backend.Data.Entities
{
    public partial class ApiScope
    {
        public ApiScope()
        {
            ApiScopeClaims = new HashSet<ApiScopeClaim>();
        }

        [Key]
        public int Id { get; set; }
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
        public int ApiResourceId { get; set; }

        [ForeignKey(nameof(ApiResourceId))]
        [InverseProperty("ApiScopes")]
        public virtual ApiResource ApiResource { get; set; }
        [InverseProperty(nameof(ApiScopeClaim.ApiScope))]
        public virtual ICollection<ApiScopeClaim> ApiScopeClaims { get; set; }
    }
}
