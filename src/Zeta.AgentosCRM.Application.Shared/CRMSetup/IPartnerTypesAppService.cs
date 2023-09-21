using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.CRMSetup.Dtos;
using Zeta.AgentosCRM.Dto;
using System.Collections.Generic;

namespace Zeta.AgentosCRM.CRMSetup
{
    public interface IPartnerTypesAppService : IApplicationService
    {
        Task<PagedResultDto<GetPartnerTypeForViewDto>> GetAll(GetAllPartnerTypesInput input);

        Task<GetPartnerTypeForViewDto> GetPartnerTypeForView(int id);

        Task<GetPartnerTypeForEditOutput> GetPartnerTypeForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditPartnerTypeDto input);

        Task Delete(EntityDto input);

        Task<FileDto> GetPartnerTypesToExcel(GetAllPartnerTypesForExcelInput input);

        Task<List<PartnerTypeMasterCategoryLookupTableDto>> GetAllMasterCategoryForTableDropdown();

    }
}