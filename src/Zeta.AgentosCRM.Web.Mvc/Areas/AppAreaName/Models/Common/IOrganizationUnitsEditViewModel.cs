using System.Collections.Generic;
using Zeta.AgentosCRM.Organizations.Dto;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Common
{
    public interface IOrganizationUnitsEditViewModel
    {
        List<OrganizationUnitDto> AllOrganizationUnits { get; set; }

        List<string> MemberedOrganizationUnits { get; set; }
    }
}