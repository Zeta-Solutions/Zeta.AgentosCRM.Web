using System;
using System.Collections.Generic;
using System.Text;

namespace Zeta.AgentosCRM.CRMApplications.Dtos
{
    public class UpdateApplicationIsDiscontinue
    {
        public long? ApplicationId { get; set; }
        public bool IsDiscontinue { get; set; }
    }
}
