using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMSetup.Tag.Dtos
{
    public class GetTagForEditOutput
    {
        public CreateOrEditTagDto Tag { get; set; }

    }
}