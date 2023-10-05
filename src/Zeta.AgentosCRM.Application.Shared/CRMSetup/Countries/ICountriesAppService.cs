using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.CRMSetup.Countries.Dtos;
using Zeta.AgentosCRM.Dto;
using System.Collections.Generic;

namespace Zeta.AgentosCRM.CRMSetup.Countries
{
    public interface ICountriesAppService : IApplicationService
    {
        Task<PagedResultDto<GetCountryForViewDto>> GetAll(GetAllCountriesInput input);

        Task<GetCountryForViewDto> GetCountryForView(int id);

        Task<GetCountryForEditOutput> GetCountryForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditCountryDto input);

        Task Delete(EntityDto input);

        Task<List<CountryRegionLookupTableDto>> GetAllRegionForTableDropdown();

    }
}