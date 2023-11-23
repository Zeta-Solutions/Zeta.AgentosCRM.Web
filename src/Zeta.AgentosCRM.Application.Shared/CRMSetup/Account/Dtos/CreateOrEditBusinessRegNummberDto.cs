using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMSetup.Account.Dtos
{
    public class CreateOrEditBusinessRegNummberDto : EntityDto<int?>
    {

        public string RegistrationNo { get; set; }

        public long OrganizationUnitId { get; set; }

    }
}