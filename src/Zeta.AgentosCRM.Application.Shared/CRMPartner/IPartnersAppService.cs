using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.CRMPartner.Dtos;
using Zeta.AgentosCRM.Dto;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Collections.Generic;

namespace Zeta.AgentosCRM.CRMPartner
{
    public interface IPartnersAppService : IApplicationService
    {
        Task<PagedResultDto<GetPartnerForViewDto>> GetAll(GetAllPartnersInput input);

        Task<GetPartnerForEditOutput> GetPartnerForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditPartnerDto input);

        Task Delete(EntityDto<long> input);

        Task<FileDto> GetPartnersToExcel(GetAllPartnersForExcelInput input);

        Task<List<PartnerBinaryObjectLookupTableDto>> GetAllBinaryObjectForTableDropdown();

        Task<List<PartnerMasterCategoryLookupTableDto>> GetAllMasterCategoryForTableDropdown();

        Task<List<PartnerPartnerTypeLookupTableDto>> GetAllPartnerTypeForTableDropdown();

        Task<List<PartnerWorkflowLookupTableDto>> GetAllWorkflowForTableDropdown();

        Task<List<PartnerCountryLookupTableDto>> GetAllCountryForTableDropdown();

        Task<List<PartnerCRMCurrencyLookupTableDto>> GetAllCRMCurrencyForTableDropdown();

    }
}