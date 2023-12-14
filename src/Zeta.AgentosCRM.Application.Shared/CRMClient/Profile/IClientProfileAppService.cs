using Abp.Application.Services;
using System;
using System.Threading.Tasks;
using Zeta.AgentosCRM.Authorization.Users.Profile.Dto;
using Zeta.AgentosCRM.CRMClient.Profile.Dto;

namespace Zeta.AgentosCRM.CRMClient.Profile
{
    public interface IClientProfileAppService : IApplicationService
    {
        Task UpdateProfilePicture(UpdateClientProfilePictureInput input);

        Task<GetProfilePictureOutput> GetProfilePictureByClient(long clientId); 
        
        Task<Guid> InsertProfilePictureForClient(UpdateClientProfilePictureInput input);

    }
}
