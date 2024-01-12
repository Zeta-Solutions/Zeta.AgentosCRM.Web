using Zeta.AgentosCRM.CRMSetup.Email;
using Zeta.AgentosCRM.Tenants.Email.Configuration.Dtos;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.EmailConfigurations
{
    public class CreateOrEditEmailConfigurationModelViewModel
    {
        public CreateOrEditEmailConfigurationDto EmailConfiguration { get; set; }

        public bool IsEditMode => EmailConfiguration.Id.HasValue;
    }
}
