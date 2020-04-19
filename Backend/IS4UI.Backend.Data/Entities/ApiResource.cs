using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IS4UI.Backend.Data.Entities
{
    public partial class ApiResource
    {
        public ApiResource()
        {
            ApiClaims = new HashSet<ApiClaim>();
            ApiProperties = new HashSet<ApiProperty>();
            ApiScopes = new HashSet<ApiScope>();
            ApiSecrets = new HashSet<ApiSecret>();
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
        [StringLength(100)]
        public string AllowedAccessTokenSigningAlgorithms { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? LastAccessed { get; set; }
        public bool NonEditable { get; set; }

        [InverseProperty(nameof(ApiClaim.ApiResource))]
        public virtual ICollection<ApiClaim> ApiClaims { get; set; }
        [InverseProperty(nameof(ApiProperty.ApiResource))]
        public virtual ICollection<ApiProperty> ApiProperties { get; set; }
        [InverseProperty(nameof(ApiScope.ApiResource))]
        public virtual ICollection<ApiScope> ApiScopes { get; set; }
        [InverseProperty(nameof(ApiSecret.ApiResource))]
        public virtual ICollection<ApiSecret> ApiSecrets { get; set; }
    }
}
