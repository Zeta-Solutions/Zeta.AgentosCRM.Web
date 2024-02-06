using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Zeta.AgentosCRM.CRMLead.Dtos
{
    public class LeadHeadDto : EntityDto<long>
    {
        public int TenantId { get; set; }
        public string FormName { get; set; }
        public string CoverImage { get; set; }
        public string Logo { get; set; }
        public string Formtittle { get; set; }
        public string HeaderNote { get; set; }
        public long? OrganizationUnitId { get; set; }
        public int? LeadSourceId { get; set; }
        public string TagName { get; set; }
        public virtual bool IsPrivacyShown { get; set; }
        public virtual string PrivacyInfo { get; set; }
        public virtual string Consent { get; set; }
    }
}
