using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.CRMClient.CheckIn.Dtos;
using Zeta.AgentosCRM.Dto;
using System.Collections.Generic;
using System.Collections.Generic;

namespace Zeta.AgentosCRM.CRMClient.CheckIn
{
    public interface ICheckInLogsAppService : IApplicationService
    {
        Task<PagedResultDto<GetCheckInLogForViewDto>> GetAll(GetAllCheckInLogsInput input);

        Task<GetCheckInLogForViewDto> GetCheckInLogForView(long id);

        Task<GetCheckInLogForEditOutput> GetCheckInLogForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditCheckInLogDto input);

        Task Delete(EntityDto<long> input);

        Task<List<CheckInLogClientLookupTableDto>> GetAllClientForTableDropdown();

        Task<List<CheckInLogUserLookupTableDto>> GetAllUserForTableDropdown();

    }
}