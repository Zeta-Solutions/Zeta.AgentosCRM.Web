﻿using Zeta.AgentosCRM.Sessions.Dto;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Layout
{
    public class SubscriptionBarViewModel
    {
        public int SubscriptionExpireNotifyDayCount { get; set; }

        public GetCurrentLoginInformationsOutput LoginInformations { get; set; }

        public string CssClass { get; set; }
    }
}
