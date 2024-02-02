using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Zeta.AgentosCRM.CRMLead.Dtos;
using Zeta.AgentosCRM.Dto;

namespace Zeta.AgentosCRM.CRMLead
{
    public interface ILeadDetailAppService : IApplicationService
    {
        Task<PagedResultDto<GetLeadDetailForViewDto>> GetAll(GetAllLeadDetailsInput input);

        //Task<PagedResultDto<GetLeadHeadForViewDto>> GetAllBYFormName(GetAllLeadHeadInput input);

        Task<GetLeadDetailForViewDto> GetLeadDetailForView(long id);

        Task<GetLeadDetailForEditOutput> GetLeadDetailForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditLeadDetailDto input);

        Task Delete(EntityDto<long> input);
        Task<FileDto> GetLeadDetailsToExcel(GetAllLeadDetailsForExcelInput input);
    }
}
