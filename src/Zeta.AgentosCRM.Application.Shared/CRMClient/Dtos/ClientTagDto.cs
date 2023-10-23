using System;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMClient.Dtos
{
    public class ClientTagDto : EntityDto
    {

        public long? ClientId { get; set; }

        public int TagId { get; set; }

    }
}