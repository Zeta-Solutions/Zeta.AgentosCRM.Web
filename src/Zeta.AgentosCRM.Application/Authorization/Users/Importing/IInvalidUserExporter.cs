using System.Collections.Generic;
using Zeta.AgentosCRM.Authorization.Users.Importing.Dto;
using Zeta.AgentosCRM.Dto;

namespace Zeta.AgentosCRM.Authorization.Users.Importing
{
    public interface IInvalidUserExporter
    {
        FileDto ExportToFile(List<ImportUserDto> userListDtos);
    }
}
