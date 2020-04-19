using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IS4UI.Backend.Data.Entities
{
    public partial class ClientPostLogoutRedirectUri
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(2000)]
        public string PostLogoutRedirectUri { get; set; }
        public int ClientId { get; set; }

        [ForeignKey(nameof(ClientId))]
        [InverseProperty("ClientPostLogoutRedirectUris")]
        public virtual Client Client { get; set; }
    }
}
