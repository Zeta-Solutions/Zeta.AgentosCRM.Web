using Abp.AutoMapper;
using Zeta.AgentosCRM.Localization.Dto;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Languages
{
    [AutoMapFrom(typeof(GetLanguageForEditOutput))]
    public class CreateOrEditLanguageModalViewModel : GetLanguageForEditOutput
    {
        public bool IsEditMode => Language.Id.HasValue;
    }
}