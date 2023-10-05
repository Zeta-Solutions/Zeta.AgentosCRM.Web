using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMSetup.InstallmentType.Dtos
{
    public class GetInstallmentTypeForEditOutput
    {
        public CreateOrEditInstallmentTypeDto InstallmentType { get; set; }

    }
}