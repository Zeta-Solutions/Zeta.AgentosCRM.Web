using System;
using System.Collections.Generic;
using System.Text;

namespace Zeta.AgentosCRM.CRMClient.Dtos
{
    public class UpdateClientAssigneeDto
    {
        public long? ClientId { get; set; }

        public long AssigneeId { get; set; } 
    }
}
