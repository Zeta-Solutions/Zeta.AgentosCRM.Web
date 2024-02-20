using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.CRMLeadInquiry.Dtos;
using Zeta.AgentosCRM.Dto;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Collections.Generic;

namespace Zeta.AgentosCRM.CRMLeadInquiry
{
    public interface ICRMInquiriesAppService : IApplicationService
    {
        Task<PagedResultDto<GetCRMInquiryForViewDto>> GetAll(GetAllCRMInquiriesInput input);

        Task<GetCRMInquiryForViewDto> GetCRMInquiryForView(long id);

        Task<GetCRMInquiryForEditOutput> GetCRMInquiryForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditCRMInquiryDto input);

        Task Delete(EntityDto<long> input);

        Task<FileDto> GetCRMInquiriesToExcel(GetAllCRMInquiriesForExcelInput input);

        Task<List<CRMInquiryCountryLookupTableDto>> GetAllCountryForTableDropdown();

        Task<List<CRMInquiryDegreeLevelLookupTableDto>> GetAllDegreeLevelForTableDropdown();

        Task<List<CRMInquirySubjectLookupTableDto>> GetAllSubjectForTableDropdown();

        Task<List<CRMInquirySubjectAreaLookupTableDto>> GetAllSubjectAreaForTableDropdown();

        Task<List<CRMInquiryOrganizationUnitLookupTableDto>> GetAllOrganizationUnitForTableDropdown();

        Task<List<CRMInquiryLeadSourceLookupTableDto>> GetAllLeadSourceForTableDropdown();

        Task<List<CRMInquiryTagLookupTableDto>> GetAllTagForTableDropdown();

        Task RemoveDocumentIdFile(EntityDto<long> input);

        Task RemovePictureIdFile(EntityDto<long> input);

    }
}