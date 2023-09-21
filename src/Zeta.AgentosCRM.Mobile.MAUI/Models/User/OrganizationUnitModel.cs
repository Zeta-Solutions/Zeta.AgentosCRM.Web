using Abp.AutoMapper;
using Zeta.AgentosCRM.Organizations.Dto;

namespace Zeta.AgentosCRM.Mobile.MAUI.Models.User
{
    [AutoMapFrom(typeof(OrganizationUnitDto))]
    public class OrganizationUnitModel : OrganizationUnitDto
    {
        public bool IsAssigned { get; set; }
    }
}
