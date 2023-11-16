using System.Collections.Generic;
using Zeta.AgentosCRM.CRMPartner.PartnerBranch.Dtos;
using Zeta.AgentosCRM.CRMSetup.Dtos;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.PartnerBranch
{
    public class CreateOrEditBranchModalViewModel
    {
        public CreateOrEditBranchDto Branch{ get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
         public string City { get; set; }
         public string Street { get; set; }
         public string State { get; set; }
         public string ZipCode { get; set; }
         public string PhoneNo { get; set; }
         public string PhoneCode { get; set; }
         public string CountryName { get; set; }
        public List<BranchCountryLookupTableDto> BranchCountryList { get; set; }
        public bool IsEditMode => Branch.Id.HasValue;

        public int PartnerId { get;set; }
    }
}
