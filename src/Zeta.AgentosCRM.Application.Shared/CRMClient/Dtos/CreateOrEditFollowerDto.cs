using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMClient.Dtos
{
    public class CreateOrEditFollowerDto : EntityDto<int?>
    {

        public long ClientId { get; set; }

        public long UserId { get; set; }

    }
}