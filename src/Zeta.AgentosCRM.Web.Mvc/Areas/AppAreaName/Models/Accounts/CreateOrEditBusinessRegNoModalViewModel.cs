 
using Zeta.AgentosCRM.CRMSetup.Account.Dtos;

using Abp.Extensions;
using System.Collections.Generic;
using Zeta.AgentosCRM.CRMAgent.Dtos;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Accounting
{
    public class CreateOrEditBusinessRegNummberModalViewModel
    {
        public CreateOrEditBusinessRegNummberDto BusinessRegNummber { get; set; }

        public bool IsEditMode => BusinessRegNummber.Id.HasValue;
        public List<BusinessRegNummberOrganizationUnitLookupTableDto> BusinessRegNummberOrganizationUnitList { get; set; }
        public string  OrganizationUnitName { get; set; }

    }
}
