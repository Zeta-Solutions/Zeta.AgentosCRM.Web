using System.Collections.Generic;
using Zeta.AgentosCRM.CRMClient.Education;
using Zeta.AgentosCRM.CRMClient.Education.Dtos;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Education
{
    public class CreateOrEditEnglishScoreViewModel
    {
        public CreateOrEditEnglisTestScoreDto EnglishTestScore {  get; set; }


        public List<EnglisTestScoreClientLookupTableDto> EnglishTestScoreList { get; set; }

        public bool IsEditMode => EnglishTestScore.Id.HasValue;
    }
}
