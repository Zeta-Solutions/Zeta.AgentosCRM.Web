using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMClient.InterstedServices.Dtos
{
    public class CreateOrEditClientInterstedServiceDto : EntityDto<long?>
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