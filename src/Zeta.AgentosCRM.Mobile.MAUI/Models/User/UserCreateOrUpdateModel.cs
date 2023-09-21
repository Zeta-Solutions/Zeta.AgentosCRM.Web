using Abp.AutoMapper;
using Zeta.AgentosCRM.Authorization.Users.Dto;

namespace Zeta.AgentosCRM.Mobile.MAUI.Models.User
{
    [AutoMapFrom(typeof(CreateOrUpdateUserInput))]
    public class UserCreateOrUpdateModel : CreateOrUpdateUserInput
    {

    }
}
