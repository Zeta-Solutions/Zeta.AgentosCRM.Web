using Abp.EntityHistory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zeta.AgentosCRM.Authorization.Users;

namespace Zeta.AgentosCRM.Auditing
{
    public class EntityChange_PropertyAndUser
    {
        public EntityChange EntityChange { get; set; }

        public User User { get; set; }

        public EntityPropertyChange EntityPropertyChange { get; set; }
    }
}
