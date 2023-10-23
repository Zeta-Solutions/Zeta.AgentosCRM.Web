using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMClient.Dtos
{
    public class GetFollowerForEditOutput
    {
        public CreateOrEditFollowerDto Follower { get; set; }

        public string ClientFirstName { get; set; }

        public string UserName { get; set; }

    }
}