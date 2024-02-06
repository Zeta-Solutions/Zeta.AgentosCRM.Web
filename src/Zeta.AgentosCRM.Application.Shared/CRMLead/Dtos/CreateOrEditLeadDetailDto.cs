using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Zeta.AgentosCRM.CRMLead.Dtos
{
    public class CreateOrEditLeadDetailDto : EntityDto<long?>
    {
        public int TenantId { get; set; }

        public  string PropertyName { get; set; }
        [Required]
        [StringLength(LeadDetailsConsts.MaxInputtypeLength, MinimumLength = LeadDetailsConsts.MinInputtypeLength)]
        public  string Inputtype { get; set; }
        public  string Status { get; set; }
        public  string SectionName { get; set; }
        public  long LeadheadId { get; set; }
    }
}
