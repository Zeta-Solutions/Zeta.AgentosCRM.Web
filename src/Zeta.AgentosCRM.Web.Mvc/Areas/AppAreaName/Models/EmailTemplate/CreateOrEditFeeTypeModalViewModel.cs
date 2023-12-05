 
using Zeta.AgentosCRM.CRMSetup.Email.Dtos;

using Abp.Extensions;
namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.EmailTemplates
{
    public class CreateOrEditEmailTemplateModalViewModel
    {
        public CreateOrEditEmailTemplateDto EmailTemplate { get; set; }

        public bool IsEditMode => EmailTemplate.Id.HasValue;

    }
}
