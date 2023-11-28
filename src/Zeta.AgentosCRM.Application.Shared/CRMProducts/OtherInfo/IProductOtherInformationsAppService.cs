using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.CRMProducts.OtherInfo.Dtos; 
using System.Collections.Generic; 

namespace Zeta.AgentosCRM.CRMProducts.OtherInfo
{
    public interface IProductOtherInformationsAppService : IApplicationService
    {
        Task<PagedResultDto<GetProductOtherInformationForViewDto>> GetAll(GetAllProductOtherInformationsInput input);

        Task<GetProductOtherInformationForViewDto> GetProductOtherInformationForView(int id);

        Task<GetProductOtherInformationForEditOutput> GetProductOtherInformationForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditProductOtherInformationDto input);

        Task Delete(EntityDto input);

        Task<List<ProductOtherInformationSubjectAreaLookupTableDto>> GetAllSubjectAreaForTableDropdown();

        Task<List<ProductOtherInformationSubjectLookupTableDto>> GetAllSubjectForTableDropdown();

        Task<PagedResultDto<ProductOtherInformationDegreeLevelLookupTableDto>> GetAllDegreeLevelForLookupTable(GetAllForLookupTableInput input);

        Task<List<ProductOtherInformationProductLookupTableDto>> GetAllProductForTableDropdown();

    }
}