using Abp.Application.Services.Dto;
using System;

namespace Zeta.AgentosCRM.CRMSetup.ServiceCategory.Dtos
{
    public class GetAllServiceCategoriesForExcelInput
    {
        public string Filter { get; set; }

        public string AbbrivationFilter { get; set; }

        public string NameFilter { get; set; }

    }
}