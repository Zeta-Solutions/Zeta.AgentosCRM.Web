using System.Collections.Generic;
using Zeta.AgentosCRM.CRMClient.Education.Dtos;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Education
{
    public class CreateOrEditEducationViewModel
    {
        public CreateOrEditClientEducationDto ClientEducation { get; set; }
        public List<ClientEducationDegreeLevelLookupTableDto> ClientEducationDegreeLevelList { get; set; }
        public List<ClientEducationSubjectLookupTableDto> ClientEducationSubjectList { get; set; }
        public List<ClientEducationSubjectAreaLookupTableDto> ClientEducationSubjectAreaList { get; set; }


        public bool IsEditMode => ClientEducation.Id.HasValue;
    }
}
