using System.Collections.Generic;
using Zeta.AgentosCRM.Editions.Dto;
using Zeta.AgentosCRM.Security;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Tenants
{
    public class CreateTenantViewModel
    {
        public IReadOnlyList<SubscribableEditionComboboxItemDto> EditionItems { get; set; }

        public PasswordComplexitySetting PasswordComplexitySetting { get; set; }

        public CreateTenantViewModel(IReadOnlyList<SubscribableEditionComboboxItemDto> editionItems)
        {
            EditionItems = editionItems;
        }
    }
}