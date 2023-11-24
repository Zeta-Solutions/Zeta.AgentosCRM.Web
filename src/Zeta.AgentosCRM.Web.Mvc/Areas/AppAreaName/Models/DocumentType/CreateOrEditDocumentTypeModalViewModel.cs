 
using Zeta.AgentosCRM.CRMSetup.Documents.Dtos;

using Abp.Extensions;
namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.DocumentTypes
{
    public class CreateOrEditDocumentTypeModalViewModel
    {
        public CreateOrEditDocumentTypeDto DocumentType { get; set; }

        public bool IsEditMode => DocumentType.Id.HasValue;

    }
}
