using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Zeta.AgentosCRM.CRMLead.Dtos;
using Zeta.AgentosCRM.CRMNotes.Dtos;
using Zeta.AgentosCRM.Dto;

namespace Zeta.AgentosCRM.CRMLead
{
    public interface ILeadHeadAppService : IApplicationService
    {
        Task<PagedResultDto<GetLeadHeadForViewDto>> GetAll(GetAllLeadHeadInput input);

        //Task<PagedResultDto<GetLeadHeadForViewDto>> GetAllBYFormName(GetAllLeadHeadInput input);

        Task<GetLeadHeadForViewDto> GetLeadHeadForView(long id);

        Task<GetLeadHeadForEditOutput> GetLeadHeadForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditLeadHeadDto input);

        Task Delete(EntityDto<long> input);
       // Task<FileDto> GetLeadHeadToExcel(GetAllLeadHeadForExcelInput input);

        Task<List<LeadOrganizationalUnitLookupTableDto>> GetAllOrganziationalUnitForTableDropdown();

        Task<List<LeadLeadSourceLookupTableDto>> GetAllLeadSourceForTableDropdown();

       // Task<List<LeadTagLookupTableDto>> GetAllTagForTableDropdown();
    }
}
