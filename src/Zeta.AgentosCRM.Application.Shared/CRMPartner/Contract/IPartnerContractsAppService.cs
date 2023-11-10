using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.CRMPartner.Contract.Dtos;
using Zeta.AgentosCRM.Dto;
using System.Collections.Generic;
using System.Collections.Generic;

namespace Zeta.AgentosCRM.CRMPartner.Contract
{
    public interface IPartnerContractsAppService : IApplicationService
    {
        Task<PagedResultDto<GetPartnerContractForViewDto>> GetAll(GetAllPartnerContractsInput input);

        Task<GetPartnerContractForViewDto> GetPartnerContractForView(int id);

        Task<GetPartnerContractForEditOutput> GetPartnerContractForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditPartnerContractDto input);

        Task Delete(EntityDto input);

        Task<List<PartnerContractAgentLookupTableDto>> GetAllAgentForTableDropdown();

        Task<List<PartnerContractRegionLookupTableDto>> GetAllRegionForTableDropdown();

    }
}