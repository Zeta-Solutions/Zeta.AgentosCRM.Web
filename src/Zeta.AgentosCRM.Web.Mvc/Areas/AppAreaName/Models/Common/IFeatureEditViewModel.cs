using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.Editions.Dto;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Common
{
    public interface IFeatureEditViewModel
    {
        List<NameValueDto> FeatureValues { get; set; }

        List<FlatFeatureDto> Features { get; set; }
    }
}