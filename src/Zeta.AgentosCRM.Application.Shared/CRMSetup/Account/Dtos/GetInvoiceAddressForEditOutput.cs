using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMSetup.Account.Dtos
{
    public class GetInvoiceAddressForEditOutput
    {
        public CreateOrEditInvoiceAddressDto InvoiceAddress { get; set; }

        public string CountryName { get; set; }

        public string OrganizationUnitDisplayName { get; set; }

    }
}