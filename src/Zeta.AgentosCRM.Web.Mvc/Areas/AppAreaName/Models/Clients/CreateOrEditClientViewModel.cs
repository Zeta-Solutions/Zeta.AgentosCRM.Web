﻿using Zeta.AgentosCRM.CRMClient.Dtos;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Collections.Generic;

using Abp.Extensions;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Clients
{
    public class CreateOrEditClientViewModel
    {
        public CreateOrEditClientDto Client { get; set; }

        public string CountryDisplayProperty { get; set; }

        public string UserName { get; set; }

        public string BinaryObjectDescription { get; set; }

        public string DegreeLevelName { get; set; }

        public string SubjectAreaName { get; set; }

        public string LeadSourceName { get; set; }

        public string CountryName2 { get; set; }

        public string CountryName3 { get; set; }

        public List<ClientCountryLookupTableDto> ClientCountryList { get; set; }

        public List<ClientUserLookupTableDto> ClientUserList { get; set; }

        public List<ClientDegreeLevelLookupTableDto> ClientDegreeLevelList { get; set; }

        public List<ClientSubjectAreaLookupTableDto> ClientSubjectAreaList { get; set; }

        public List<ClientLeadSourceLookupTableDto> ClientLeadSourceList { get; set; }

        public bool IsEditMode => Client.Id.HasValue;
    }
}