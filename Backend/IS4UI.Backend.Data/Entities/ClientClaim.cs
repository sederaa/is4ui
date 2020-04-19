using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IS4UI.Backend.Data.Entities
{
    public partial class ClientClaim
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(250)]
        public string Type { get; set; }
        [Required]
        [StringLength(250)]
        public string Value { get; set; }
        public int ClientId { get; set; }

        [ForeignKey(nameof(ClientId))]
        [InverseProperty("ClientClaims")]
        public virtual Client Client { get; set; }
    }
}
