using Zeta.AgentosCRM.CRMSetup.InstallmentType.Dtos;

using Abp.Extensions;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.InstallmentType
{
    public class CreateOrEditInstallmentTypeModalViewModel
    {
        public CreateOrEditInstallmentTypeDto InstallmentType { get; set; }

        public bool IsEditMode=>InstallmentType.Id.HasValue;
    }
}
