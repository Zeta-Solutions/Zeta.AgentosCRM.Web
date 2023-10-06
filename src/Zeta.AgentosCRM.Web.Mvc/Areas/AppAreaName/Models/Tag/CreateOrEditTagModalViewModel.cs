using Zeta.AgentosCRM.CRMSetup.Tag.Dtos;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Tag
{
    public class CreateOrEditTagModalViewModel
    {
        public CreateOrEditTagDto Tag { get; set;}

        public bool IsEditMode => Tag.Id.HasValue;
    }
}
