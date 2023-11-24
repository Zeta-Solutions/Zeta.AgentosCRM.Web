using System.Collections.Generic;
using Zeta.AgentosCRM.CRMClient.Education.Dtos;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Education
{
    public class CreateOrEditOtherTestScoresViewModel
    {
        public CreateOrEditOtherTestScoreDto OtherTestScore { get; set; }


        public List<OtherTestScoreClientLookupTableDto> OtherTestScoreList { get; set; }

        public bool IsEditMode => OtherTestScore.Id.HasValue;
    }
}
