using System.Collections.Generic;
using Abp;
using Zeta.AgentosCRM.Chat.Dto;
using Zeta.AgentosCRM.Dto;

namespace Zeta.AgentosCRM.Chat.Exporting
{
    public interface IChatMessageListExcelExporter
    {
        FileDto ExportToFile(UserIdentifier user, List<ChatMessageExportDto> messages);
    }
}
