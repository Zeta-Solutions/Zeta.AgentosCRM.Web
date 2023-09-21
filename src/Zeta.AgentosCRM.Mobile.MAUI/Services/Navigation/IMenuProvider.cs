using Zeta.AgentosCRM.Models.NavigationMenu;

namespace Zeta.AgentosCRM.Services.Navigation
{
    public interface IMenuProvider
    {
        List<NavigationMenuItem> GetAuthorizedMenuItems(Dictionary<string, string> grantedPermissions);
    }
}