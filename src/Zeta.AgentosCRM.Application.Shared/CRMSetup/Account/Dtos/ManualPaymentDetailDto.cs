using System;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMSetup.Account.Dtos
{
    public class ManualPaymentDetailDto : EntityDto
    {
        public string Name { get; set; }

        public string PaymentDetail { get; set; }

        public long OrganizationUnitId { get; set; }

    }
}