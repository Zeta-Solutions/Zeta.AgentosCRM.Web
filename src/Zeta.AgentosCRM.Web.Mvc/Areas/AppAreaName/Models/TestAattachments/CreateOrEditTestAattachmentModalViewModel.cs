using Zeta.AgentosCRM.AttachmentTest.Dtos;

using Abp.Extensions;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.TestAattachments
{
    public class CreateOrEditTestAattachmentModalViewModel
    {
        public CreateOrEditTestAattachmentDto TestAattachment { get; set; }

        public string AttachmentFileName { get; set; }
        public string AttachmentFileAcceptedTypes { get; set; }

        public bool IsEditMode => TestAattachment.Id.HasValue;
    }
}