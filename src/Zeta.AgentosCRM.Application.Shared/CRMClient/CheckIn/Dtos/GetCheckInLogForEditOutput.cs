using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMClient.CheckIn.Dtos
{
    public class GetCheckInLogForEditOutput
    {
        public CreateOrEditCheckInLogDto CheckInLog { get; set; }

        public string ClientDisplayProperty { get; set; }

        public string UserName { get; set; }

    }
}