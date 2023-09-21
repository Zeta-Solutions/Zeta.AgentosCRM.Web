using System.Collections.Generic;
using Zeta.AgentosCRM.Authorization.Permissions.Dto;

namespace Zeta.AgentosCRM.Authorization.Users.Dto
{
    public class GetUserPermissionsForEditOutput
    {
        public List<FlatPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}