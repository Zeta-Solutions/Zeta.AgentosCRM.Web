using Zeta.AgentosCRM.Dto;

namespace Zeta.AgentosCRM.Common.Dto
{
    public class FindUsersInput : PagedAndFilteredInputDto
    {
        public int? TenantId { get; set; }

        public bool ExcludeCurrentUser { get; set; }
    }
}