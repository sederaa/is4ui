using System;
using System.Collections.Generic;

namespace IS4UI.Backend.Data.Entities
{
    public partial class ClientCorsOrigin
    {
        public int Id { get; set; }
        public string Origin { get; set; }
        public int ClientId { get; set; }

        public virtual Client Client { get; set; }
    }
}
