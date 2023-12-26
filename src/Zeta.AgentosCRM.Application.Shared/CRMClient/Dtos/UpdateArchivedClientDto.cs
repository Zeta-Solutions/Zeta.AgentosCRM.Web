using System;
using System.Collections.Generic;
using System.Text;

namespace Zeta.AgentosCRM.CRMClient.Dtos
{
    public class UpdateArchivedClientDto
    { 
        public long? ClientId { get; set; }

        public bool IsArchived { get; set; } = false;
    }
}
