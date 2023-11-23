using System;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMSetup.Account.Dtos
{
    public class BusinessRegNummberDto : EntityDto
    {
        public string RegistrationNo { get; set; }

        public long OrganizationUnitId { get; set; }

    }
}