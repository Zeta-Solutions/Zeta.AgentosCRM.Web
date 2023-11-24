using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.CRMSetup.Account.Dtos;
using Zeta.AgentosCRM.Dto;
using System.Collections.Generic;

namespace Zeta.AgentosCRM.CRMSetup.Account
{
    public interface IBusinessRegNummbersAppService : IApplicationService
    {
        Task<PagedResultDto<GetBusinessRegNummberForViewDto>> GetAll(GetAllBusinessRegNummbersInput input);

        Task<GetBusinessRegNummberForViewDto> GetBusinessRegNummberForView(int id);

        Task<GetBusinessRegNummberForEditOutput> GetBusinessRegNummberForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditBusinessRegNummberDto input);

        Task Delete(EntityDto input);

        Task<List<BusinessRegNummberOrganizationUnitLookupTableDto>> GetAllOrganizationUnitForTableDropdown();

    }
}