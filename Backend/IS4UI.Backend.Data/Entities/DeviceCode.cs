﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IS4UI.Backend.Data.Entities
{
    public partial class DeviceCode
    {
        [Key]
        [StringLength(200)]
        public string UserCode { get; set; }
        [Required]
        [Column("DeviceCode")]
        [StringLength(200)]
        public string DeviceCode1 { get; set; }
        [StringLength(200)]
        public string SubjectId { get; set; }
        [Required]
        [StringLength(200)]
        public string ClientId { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime Expiration { get; set; }
        [Required]
        public string Data { get; set; }
    }
}
