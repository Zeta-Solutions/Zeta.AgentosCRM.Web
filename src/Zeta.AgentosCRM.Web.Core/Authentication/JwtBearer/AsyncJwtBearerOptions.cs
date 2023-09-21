using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Zeta.AgentosCRM.Web.Authentication.JwtBearer
{
    public class AsyncJwtBearerOptions : JwtBearerOptions
    {
        public readonly List<IAsyncSecurityTokenValidator> AsyncSecurityTokenValidators;
        
        private readonly AgentosCRMAsyncJwtSecurityTokenHandler _defaultAsyncHandler = new AgentosCRMAsyncJwtSecurityTokenHandler();

        public AsyncJwtBearerOptions()
        {
            AsyncSecurityTokenValidators = new List<IAsyncSecurityTokenValidator>() {_defaultAsyncHandler};
        }
    }

}
