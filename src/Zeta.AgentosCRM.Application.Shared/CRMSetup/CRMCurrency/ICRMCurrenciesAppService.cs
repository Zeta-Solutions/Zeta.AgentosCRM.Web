using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.CRMSetup.CRMCurrency.Dtos;
using Zeta.AgentosCRM.Dto;

namespace Zeta.AgentosCRM.CRMSetup.CRMCurrency
{
    public interface ICRMCurrenciesAppService : IApplicationService
    {
        Task<PagedResultDto<GetCRMCurrencyForViewDto>> GetAll(GetAllCRMCurrenciesInput input);

        Task<GetCRMCurrencyForEditOutput> GetCRMCurrencyForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditCRMCurrencyDto input);

        Task Delete(EntityDto input);

    }
}