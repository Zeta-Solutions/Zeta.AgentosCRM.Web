using System.Collections.Generic;
using Zeta.AgentosCRM.Authorization.Users.Importing.Dto;
using Abp.Dependency;

namespace Zeta.AgentosCRM.Authorization.Users.Importing
{
    public interface IUserListExcelDataReader: ITransientDependency
    {
        List<ImportUserDto> GetUsersFromExcel(byte[] fileBytes);
    }
}
