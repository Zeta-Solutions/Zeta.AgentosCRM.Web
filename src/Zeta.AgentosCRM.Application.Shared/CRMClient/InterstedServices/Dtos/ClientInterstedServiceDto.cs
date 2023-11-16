using System;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMClient.InterstedServices.Dtos
{
    public class ClientInterstedServiceDto : EntityDto<long>
    {
        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public long ClientId { get; set; }

        public long PartnerId { get; set; }

        public long ProductId { get; set; }

        public long BranchId { get; set; }

        public int WorkflowId { get; set; }

    }
}