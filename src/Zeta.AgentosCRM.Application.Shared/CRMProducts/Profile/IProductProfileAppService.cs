using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Zeta.AgentosCRM.Authorization.Users.Profile.Dto; 
using Zeta.AgentosCRM.CRMProducts.Profile.Dtos;

namespace Zeta.AgentosCRM.CRMProducts.Profile
{
    public interface IProductProfileAppService : IApplicationService
    {
        Task UpdateProfilePicture(UpdateProductProfilePictureInput input);

        Task<GetProfilePictureOutput> GetProfilePictureByProduct(long productId);

        Task<Guid> InsertProfilePictureForProduct(UpdateProductProfilePictureInput input);

    }
}
