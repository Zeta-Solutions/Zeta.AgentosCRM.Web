using System.Collections.Generic;
using Zeta.AgentosCRM.Authorization.Users.Dto;
using Zeta.AgentosCRM.Dto;

namespace Zeta.AgentosCRM.Authorization.Users.Exporting
{
    public interface IUserListExcelExporter
    {
        FileDto ExportToFile(List<UserListDto> userListDtos);
    }
}