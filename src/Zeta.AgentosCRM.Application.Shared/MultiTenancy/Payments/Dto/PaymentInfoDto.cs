﻿using Zeta.AgentosCRM.Editions.Dto;

namespace Zeta.AgentosCRM.MultiTenancy.Payments.Dto
{
    public class PaymentInfoDto
    {
        public EditionSelectDto Edition { get; set; }

        public decimal AdditionalPrice { get; set; }

        public bool IsLessThanMinimumUpgradePaymentAmount()
        {
            return AdditionalPrice < AgentosCRMConsts.MinimumUpgradePaymentAmount;
        }
    }
}
