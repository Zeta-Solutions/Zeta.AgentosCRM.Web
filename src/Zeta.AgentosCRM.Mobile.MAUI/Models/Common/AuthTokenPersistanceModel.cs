using Abp.AutoMapper;
using Zeta.AgentosCRM.Sessions.Dto;

namespace Zeta.AgentosCRM.Models.Common
{
    [AutoMapFrom(typeof(ApplicationInfoDto)),
     AutoMapTo(typeof(ApplicationInfoDto))]
    public class ApplicationInfoPersistanceModel
    {
        public string Version { get; set; }

        public DateTime ReleaseDate { get; set; }
    }
}