using Zeta.AgentosCRM.CRMSetup.Dtos;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.SubjectArea
{
    public class CreateOrEditSubjectAreaModalViewModel
    {
        public CreateOrEditSubjectAreaDto SubjectArea { get; set; }

        public bool IsEditMode => SubjectArea.Id.HasValue;
    }
}
