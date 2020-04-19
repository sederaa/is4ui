using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IS4UI.Backend.Data.Entities
{
    public partial class ClientGrantType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(250)]
        public string GrantType { get; set; }
        public int ClientId { get; set; }

        [ForeignKey(nameof(ClientId))]
        [InverseProperty("ClientGrantTypes")]
        public virtual Client Client { get; set; }
    }
}
