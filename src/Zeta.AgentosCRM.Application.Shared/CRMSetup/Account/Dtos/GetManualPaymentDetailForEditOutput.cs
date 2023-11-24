using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMSetup.Account.Dtos
{
    public class GetManualPaymentDetailForEditOutput
    {
        public CreateOrEditManualPaymentDetailDto ManualPaymentDetail { get; set; }

        public string OrganizationUnitDisplayName { get; set; }

    }
}