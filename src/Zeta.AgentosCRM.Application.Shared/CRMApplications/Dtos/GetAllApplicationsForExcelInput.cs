﻿using Abp.Application.Services.Dto;
using System;

namespace Zeta.AgentosCRM.CRMApplications.Dtos
{
    public class GetAllApplicationsForExcelInput
    {
        public string Filter { get; set; }

        public string ClientFirstNameFilter { get; set; }

        public string WorkflowNameFilter { get; set; }

        public string PartnerPartnerNameFilter { get; set; }

        public string ProductNameFilter { get; set; }

    }
}