using Zeta.AgentosCRM.CRMClient.Documents.Dtos;
using Zeta.AgentosCRM.CRMSetup.Documents;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.DocumentAttachment
{
    public class CreateOrEditDocumentsViewModel
    {
        public CreateOrEditClientAttachmentDto ClientAttachment { get; set; }
        public bool IsEditMode => ClientAttachment.Id.HasValue;
    }
}
