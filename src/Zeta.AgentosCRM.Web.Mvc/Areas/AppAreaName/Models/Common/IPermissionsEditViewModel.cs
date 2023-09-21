using System.Collections.Generic;
using Zeta.AgentosCRM.Authorization.Permissions.Dto;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }

        List<string> GrantedPermissionNames { get; set; }
    }
}