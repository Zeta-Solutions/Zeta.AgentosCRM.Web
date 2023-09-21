using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.Editions.Dto;

namespace Zeta.AgentosCRM.MultiTenancy.Dto
{
    public class GetTenantFeaturesEditOutput
    {
        public List<NameValueDto> FeatureValues { get; set; }

        public List<FlatFeatureDto> Features { get; set; }
    }
}