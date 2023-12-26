using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.CRMPartner.PartnerBranch.Dtos;
using Zeta.AgentosCRM.Dto;
using System.Collections.Generic;

namespace Zeta.AgentosCRM.CRMPartner.PartnerBranch
{
    public interface IBranchesAppService : IApplicationService
    {
        Task<PagedResultDto<GetBranchForViewDto>> GetAll(GetAllBranchesInput input);

        Task<GetBranchForViewDto> GetBranchForView(long id);

        Task<List<GetBranchForViewDto>> GetBranchbyWorkflowId(long workflowId);

        Task<GetBranchForEditOutput> GetBranchForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditBranchDto input);

        Task Delete(EntityDto<long> input);

        Task<List<BranchCountryLookupTableDto>> GetAllCountryForTableDropdown();

    }
}