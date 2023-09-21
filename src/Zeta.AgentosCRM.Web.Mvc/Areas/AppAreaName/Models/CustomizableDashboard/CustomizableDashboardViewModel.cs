using Zeta.AgentosCRM.DashboardCustomization;
using Zeta.AgentosCRM.DashboardCustomization.Dto;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.CustomizableDashboard
{
    public class CustomizableDashboardViewModel
    {
        public DashboardOutput DashboardOutput { get; }

        public Dashboard UserDashboard { get; }

        public CustomizableDashboardViewModel(
            DashboardOutput dashboardOutput,
            Dashboard userDashboard)
        {
            DashboardOutput = dashboardOutput;
            UserDashboard = userDashboard;
        }
    }
}