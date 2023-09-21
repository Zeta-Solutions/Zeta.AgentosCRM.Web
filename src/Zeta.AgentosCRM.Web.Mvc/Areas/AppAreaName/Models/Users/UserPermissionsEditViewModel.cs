using Abp.AutoMapper;
using Zeta.AgentosCRM.Authorization.Users;
using Zeta.AgentosCRM.Authorization.Users.Dto;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Common;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Users
{
    [AutoMapFrom(typeof(GetUserPermissionsForEditOutput))]
    public class UserPermissionsEditViewModel : GetUserPermissionsForEditOutput, IPermissionsEditViewModel
    {
        public User User { get; set; }
    }
}