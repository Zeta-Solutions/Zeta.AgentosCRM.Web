using System.Threading.Tasks;
using Abp.Application.Services;
using Zeta.AgentosCRM.Editions.Dto;
using Zeta.AgentosCRM.MultiTenancy.Dto;

namespace Zeta.AgentosCRM.MultiTenancy
{
    public interface ITenantRegistrationAppService: IApplicationService
    {
        Task<RegisterTenantOutput> RegisterTenant(RegisterTenantInput input);

        Task<EditionsSelectOutput> GetEditionsForSelect();

        Task<EditionSelectDto> GetEdition(int editionId);
    }
}