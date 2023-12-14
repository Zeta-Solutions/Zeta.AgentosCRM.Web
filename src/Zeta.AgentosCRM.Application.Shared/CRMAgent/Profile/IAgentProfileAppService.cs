using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Zeta.AgentosCRM.Authorization.Users.Profile.Dto;
using Zeta.AgentosCRM.CRMAgent.Profile.Dtos;

namespace Zeta.AgentosCRM.CRMAgent.Profile
{
    public interface IAgentProfileAppService : IApplicationService
    {
        Task UpdateProfilePicture(UpdateAgentProfilePictureInput input);

        Task<GetProfilePictureOutput> GetProfilePictureByAgent(long agentId);

        Task<Guid> InsertProfilePictureForAgent(UpdateAgentProfilePictureInput input);

    }
}
