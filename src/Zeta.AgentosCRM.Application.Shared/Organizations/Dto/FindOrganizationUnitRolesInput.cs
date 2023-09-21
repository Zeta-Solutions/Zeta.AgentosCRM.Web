using Zeta.AgentosCRM.Dto;

namespace Zeta.AgentosCRM.Organizations.Dto
{
    public class FindOrganizationUnitRolesInput : PagedAndFilteredInputDto
    {
        public long OrganizationUnitId { get; set; }
    }
}