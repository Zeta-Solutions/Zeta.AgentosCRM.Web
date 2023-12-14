using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Zeta.AgentosCRM.Authorization.Users.Profile.Dto;
using Zeta.AgentosCRM.CRMPartner.Profile.Dtos;

namespace Zeta.AgentosCRM.CRMPartner.Profile
{
    public interface IPartnerProfileAppService : IApplicationService
    {
        Task UpdateProfilePicture(UpdatePartnerProfilePictureInput input);

        Task<GetProfilePictureOutput> GetProfilePictureByPartner(long partnerId);

        Task<Guid> InsertProfilePictureForPartner(UpdatePartnerProfilePictureInput input);

    }
}
