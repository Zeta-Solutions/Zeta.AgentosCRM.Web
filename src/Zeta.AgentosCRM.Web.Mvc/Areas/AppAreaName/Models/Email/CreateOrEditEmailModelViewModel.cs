using Zeta.AgentosCRM.Tenants.Email.Dtos;
using System.Collections.Generic;
namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Email
{
    public class CreateOrEditEmailModelViewModel
    {
        public CreateOrEditSentEmailDto SentEmail { get; set; }

        public bool IsEditMode => SentEmail.Id.HasValue;
        public string EmailConfiguration { get; set; }
        public string EmailTemplate { get; set; }
        public string ClientName { get; set; }
        public string ApplicationName { get; set; }

        public List<SentEmailEmailConfigurationLookupTableDto> SentEmailEmailConfigurationList { get; set; }
        public List<SentEmailEmailTemplateLookupTableDto> SentEmailEmailTemplateList { get; set; }
        public List<SentEmailClientLookupTableDto> SentEmailClientList { get; set; }
        public List<SentEmailApplicationLookupTableDto> SentEmailApplicationList { get; set; }
        
    }
}
