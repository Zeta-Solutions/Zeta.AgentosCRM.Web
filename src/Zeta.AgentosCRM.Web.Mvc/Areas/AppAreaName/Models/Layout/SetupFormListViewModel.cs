using Abp.Application.Navigation;
using Abp.Localization;
using System.Collections.Generic;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Layout
{
    public class SetupFormListViewModel
    {

        public List<MenuItemDefinition> SetupForm = new List<MenuItemDefinition>();
        //{ get; set; }
        public string CssClass { get; set; }

        public string IconClass { get; set; }
    }
}
