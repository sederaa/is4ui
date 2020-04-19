﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IS4UI.Backend.Data.Entities
{
    public partial class ClientScope
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Scope { get; set; }
        public int ClientId { get; set; }

        [ForeignKey(nameof(ClientId))]
        [InverseProperty("ClientScopes")]
        public virtual Client Client { get; set; }
    }
}
