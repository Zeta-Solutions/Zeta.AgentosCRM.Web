using Abp.Application.Services.Dto;
using System.Collections.Generic;
using Zeta.AgentosCRM.CRMProducts.OtherInfo.Dtos;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Product.Otherinfo
{
    public class CreateOrEditProductOtherInfoModalViewModel
    {
        public CreateOrEditProductOtherInformationDto ProductOtherInformation {  get; set; }
        public string SubjectAreaName { get; set; }
        public string SubjectName { get; set; }
        public string DegreeLevelName { get; set; }

        public List<ProductOtherInformationSubjectAreaLookupTableDto> ProductOtherInformationSubjectAreaList { get; set; }
        public List<ProductOtherInformationSubjectLookupTableDto> ProductOtherInformationSubjectList { get; set; }
        public List<ProductOtherInformationDegreeLevelLookupTableDto> ProductOtherInformationDegreeLevelList { get; set; }
        public bool IsEditMode => ProductOtherInformation.Id.HasValue;
    }
}
