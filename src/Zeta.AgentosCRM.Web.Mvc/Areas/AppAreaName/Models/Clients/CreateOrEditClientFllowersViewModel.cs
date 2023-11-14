using System.Collections.Generic;
using Zeta.AgentosCRM.CRMClient.Dtos;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Clients
{
    public class CreateOrEditClientFllowersViewModel
    {
        public CreateOrEditFollowerDto clientFolllowers { get; set; }
        public List<FollowerUserLookupTableDto> clientFolllowersList {  get; set; }    

        public bool IsEditMode => clientFolllowers.Id.HasValue;

    }
}
