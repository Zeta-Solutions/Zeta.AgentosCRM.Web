using System.Collections.Generic;
using System.Threading.Tasks;
using Abp;
using Zeta.AgentosCRM.Dto;

namespace Zeta.AgentosCRM.Gdpr
{
    public interface IUserCollectedDataProvider
    {
        Task<List<FileDto>> GetFiles(UserIdentifier user);
    }
}
