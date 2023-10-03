using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.CRMSetup.FeeType.Dtos;
using Zeta.AgentosCRM.Dto;

namespace Zeta.AgentosCRM.CRMSetup.FeeType
{
    public interface IFeeTypesAppService : IApplicationService
    {
        Task<PagedResultDto<GetFeeTypeForViewDto>> GetAll(GetAllFeeTypesInput input);

        Task<GetFeeTypeForViewDto> GetFeeTypeForView(int id);

        Task<GetFeeTypeForEditOutput> GetFeeTypeForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditFeeTypeDto input);

        Task Delete(EntityDto input);

    }
}