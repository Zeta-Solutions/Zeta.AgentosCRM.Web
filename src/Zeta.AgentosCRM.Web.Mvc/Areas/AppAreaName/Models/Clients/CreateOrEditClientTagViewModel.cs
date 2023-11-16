using System.Collections.Generic;
using Zeta.AgentosCRM.CRMClient.Dtos;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Clients
{
    public class CreateOrEditClientTagViewModel
    {
        
        public CreateOrEditClientTagDto ClientTag { get; set; }
        public List<ClientTagTagLookupTableDto> ClientTagList { get; set; }

        public bool IsEditMode => ClientTag.Id.HasValue;

    }
}
