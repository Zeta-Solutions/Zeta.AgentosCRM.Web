using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.TaskManagement.Dtos
{
    public class GetCRMTaskForEditOutput
    {
        public CreateOrEditCRMTaskDto CRMTask { get; set; }

        public string TaskCategoryName { get; set; }

        public string UserName { get; set; }

        public string TaskPriorityName { get; set; }

        public string ClientDisplayProperty { get; set; }

        public string PartnerPartnerName { get; set; }

        public string ApplicationName { get; set; }

        public string ApplicationStageName { get; set; }

        public string AttachmentFileName { get; set; }

    }
}