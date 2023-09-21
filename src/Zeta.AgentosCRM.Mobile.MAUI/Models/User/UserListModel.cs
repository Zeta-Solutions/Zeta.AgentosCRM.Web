using Abp.AutoMapper;
using Zeta.AgentosCRM.Authorization.Users.Dto;

namespace Zeta.AgentosCRM.Mobile.MAUI.Models.User
{
    [AutoMapFrom(typeof(UserListDto))]
    public class UserListModel : UserListDto
    {
        public string Photo { get; set; }

        public string FullName => Name + " " + Surname;
    }
}
