using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMApplications.Dtos
{
    public class CreateOrEditApplicationDto : EntityDto<long?>
    {
        [Required]
        public string Name { get; set; }

        public long ClientId { get; set; }

        public int WorkflowId { get; set; }

        public long PartnerId { get; set; }

        public long ProductId { get; set; }

        public bool? IsDiscontinue { get; set; }

    }
}