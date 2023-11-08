using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.CRMPartner.Contact.Dtos;
using Zeta.AgentosCRM.Dto;
using System.Collections.Generic;
using System.Collections.Generic;

namespace Zeta.AgentosCRM.CRMPartner.Contact
{
    public interface IPartnerContactsAppService : IApplicationService
    {
        Task<PagedResultDto<GetPartnerContactForViewDto>> GetAll(GetAllPartnerContactsInput input);

        Task<GetPartnerContactForViewDto> GetPartnerContactForView(long id);

        Task<GetPartnerContactForEditOutput> GetPartnerContactForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditPartnerContactDto input);

        Task Delete(EntityDto<long> input);

        Task<List<PartnerContactBranchLookupTableDto>> GetAllBranchForTableDropdown();

        Task<List<PartnerContactPartnerLookupTableDto>> GetAllPartnerForTableDropdown();

    }
}