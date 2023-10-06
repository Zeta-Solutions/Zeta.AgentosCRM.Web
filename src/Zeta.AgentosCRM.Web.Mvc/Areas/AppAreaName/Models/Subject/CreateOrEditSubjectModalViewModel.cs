using System.Collections.Generic;
using Zeta.AgentosCRM.CRMSetup.Dtos;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Subject
{
    public class CreateOrEditSubjectModalViewModel
    {
        public CreateOrEditSubjectDto Subject { get; set; }
        public string SubjectAreaName { get; set; }

        public List<SubjectSubjectAreaLookupTableDto> SubjectSubjectAreaList { get; set; }
        public bool IsEditMode => Subject.Id.HasValue;
    }
}
