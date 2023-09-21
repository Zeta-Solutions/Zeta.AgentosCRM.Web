using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.Authorization.Permissions.Dto;

namespace Zeta.AgentosCRM.Authorization.Permissions
{
    public interface IPermissionAppService : IApplicationService
    {
        ListResultDto<FlatPermissionWithLevelDto> GetAllPermissions();
    }
}
