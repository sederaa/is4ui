using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IS4UI.Backend.Data.Entities
{
    public partial class ClientSecret
    {
        [Key]
        public int Id { get; set; }
        [StringLength(2000)]
        public string Description { get; set; }
        [Required]
        [StringLength(4000)]
        public string Value { get; set; }
        public DateTime? Expiration { get; set; }
        [Required]
        [StringLength(250)]
        public string Type { get; set; }
        public DateTime Created { get; set; }
        public int ClientId { get; set; }

        [ForeignKey(nameof(ClientId))]
        [InverseProperty("ClientSecrets")]
        public virtual Client Client { get; set; }
    }
}
