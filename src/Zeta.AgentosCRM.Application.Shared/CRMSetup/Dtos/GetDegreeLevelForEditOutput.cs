using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMSetup.Dtos
{
    public class GetDegreeLevelForEditOutput
    {
        public CreateOrEditDegreeLevelDto DegreeLevel { get; set; }

    }
}