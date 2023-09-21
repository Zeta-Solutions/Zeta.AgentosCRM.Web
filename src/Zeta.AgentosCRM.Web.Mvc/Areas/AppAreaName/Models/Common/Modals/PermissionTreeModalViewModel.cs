using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zeta.AgentosCRM.Authorization.Permissions.Dto;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Common.Modals
{
    public class PermissionTreeModalViewModel : IPermissionsEditViewModel
    {
        public List<FlatPermissionDto> Permissions { get; set; }
        public List<string> GrantedPermissionNames { get; set; }
    }
}
