using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IS4UI.Backend.Data.Entities
{
    public partial class ClientCorsOrigin
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string Origin { get; set; }
        public int ClientId { get; set; }

        [ForeignKey(nameof(ClientId))]
        [InverseProperty("ClientCorsOrigins")]
        public virtual Client Client { get; set; }
    }
}
