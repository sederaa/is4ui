using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IS4UI.Backend.Data.Entities
{
    [Table("ClientIdPRestrictions")]
    public partial class ClientIdPrestriction
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Provider { get; set; }
        public int ClientId { get; set; }

        [ForeignKey(nameof(ClientId))]
        [InverseProperty("ClientIdPrestrictions")]
        public virtual Client Client { get; set; }
    }
}
