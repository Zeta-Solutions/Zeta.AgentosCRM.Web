using System.Collections.Generic;
using Zeta.AgentosCRM.CRMProducts.Fee.Dtos;
using Zeta.AgentosCRM.Dto;

namespace Zeta.AgentosCRM.CRMProducts.Fee.Exporting
{
    public interface IProductFeeDetailsExcelExporter
    {
        FileDto ExportToFile(List<GetProductFeeDetailForViewDto> productFeeDetails);
    }
}