using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.CRMSetup.InstallmentType.Dtos;
using Zeta.AgentosCRM.Dto;

namespace Zeta.AgentosCRM.CRMSetup.InstallmentType
{
    public interface IInstallmentTypesAppService : IApplicationService
    {
        Task<PagedResultDto<GetInstallmentTypeForViewDto>> GetAll(GetAllInstallmentTypesInput input);

        Task<GetInstallmentTypeForViewDto> GetInstallmentTypeForView(int id);

        Task<GetInstallmentTypeForEditOutput> GetInstallmentTypeForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditInstallmentTypeDto input);

        Task Delete(EntityDto input);

    }
}