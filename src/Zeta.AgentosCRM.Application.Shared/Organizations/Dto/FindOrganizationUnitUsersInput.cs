using Zeta.AgentosCRM.Dto;

namespace Zeta.AgentosCRM.Organizations.Dto
{
    public class FindOrganizationUnitUsersInput : PagedAndFilteredInputDto
    {
        public long OrganizationUnitId { get; set; }
    }
}
