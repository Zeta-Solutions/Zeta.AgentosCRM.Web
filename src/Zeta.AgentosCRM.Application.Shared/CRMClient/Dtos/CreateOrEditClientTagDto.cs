using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMClient.Dtos
{
    public class CreateOrEditClientTagDto : EntityDto<int?>
    {

        public long? ClientId { get; set; }

        public int TagId { get; set; }

    }
}