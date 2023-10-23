using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMClient.Dtos
{
    public class GetClientTagForEditOutput
    {
        public CreateOrEditClientTagDto ClientTag { get; set; }

        public string ClientFirstName { get; set; }

        public string TagName { get; set; }

    }
}