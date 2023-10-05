using Zeta.AgentosCRM.CRMSetup.Dtos;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Subject
{
    public class CreateOrEditSubjectModalViewModel
    {
        public CreateOrEditSubjectDto Subject { get; set; }
        public bool IsEditMode => Subject.Id.HasValue;
    }
}
