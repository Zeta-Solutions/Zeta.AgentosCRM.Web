using System.Collections.Generic;
using Zeta.AgentosCRM.CRMClient.CheckIn.Dtos;
using Zeta.AgentosCRM.CRMClient.InterstedServices;
using Zeta.AgentosCRM.CRMClient.InterstedServices.Dtos;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Clients.CheckInLogs
{
    public class CreateOrEditCheckInLogsViewModel
    {
        public CreateOrEditCheckInLogDto CheckInLog {  get; set; }


        public List<CheckInLogClientLookupTableDto> CheckInLogClientList { get; set; }
        public List<CheckInLogUserLookupTableDto> CheckInLogUserList { get; set; }
        public bool IsEditMode => CheckInLog.Id.HasValue;
    }
}
