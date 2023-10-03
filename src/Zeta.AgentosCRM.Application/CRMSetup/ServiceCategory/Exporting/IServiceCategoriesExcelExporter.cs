using System.Collections.Generic;
using Zeta.AgentosCRM.CRMSetup.ServiceCategory.Dtos;
using Zeta.AgentosCRM.Dto;

namespace Zeta.AgentosCRM.CRMSetup.ServiceCategory.Exporting
{
    public interface IServiceCategoriesExcelExporter
    {
        FileDto ExportToFile(List<GetServiceCategoryForViewDto> serviceCategories);
    }
}