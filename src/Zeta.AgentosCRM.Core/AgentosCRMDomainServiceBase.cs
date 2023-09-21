using Abp.Domain.Services;

namespace Zeta.AgentosCRM
{
    public abstract class AgentosCRMDomainServiceBase : DomainService
    {
        /* Add your common members for all your domain services. */

        protected AgentosCRMDomainServiceBase()
        {
            LocalizationSourceName = AgentosCRMConsts.LocalizationSourceName;
        }
    }
}
