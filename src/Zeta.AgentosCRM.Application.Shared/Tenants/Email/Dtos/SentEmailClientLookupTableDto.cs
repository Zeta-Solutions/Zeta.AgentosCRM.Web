using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.Tenants.Email.Dtos
{
    public class SentEmailClientLookupTableDto
    {
        public long Id { get; set; }

        public string DisplayName { get; set; }
    }
}