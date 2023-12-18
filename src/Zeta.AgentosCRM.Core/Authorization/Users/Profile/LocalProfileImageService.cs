using System;
using System.Threading.Tasks;
using Abp;
using Abp.Dependency;
using Abp.Domain.Repositories; 
using Zeta.AgentosCRM.CRMAgent;
using Zeta.AgentosCRM.CRMClient;
using Zeta.AgentosCRM.CRMPartner;
using Zeta.AgentosCRM.CRMProducts;
using Zeta.AgentosCRM.Storage;

namespace Zeta.AgentosCRM.Authorization.Users.Profile
{
    public class LocalProfileImageService : IProfileImageService, ITransientDependency
    {
        private readonly IBinaryObjectManager _binaryObjectManager;
        private readonly UserManager _userManager;
        private readonly IRepository<Client, long> _clientRepository;
        private readonly IRepository<Agent, long> _agentRepository;
        private readonly IRepository<Partner, long> _partnerRepository;
        private readonly IRepository<Product, long> _productRepository;

        public LocalProfileImageService(
            IBinaryObjectManager binaryObjectManager,
            UserManager userManager,
            IRepository<Client, long> clientRepository,
            IRepository<Agent, long> agentRepository,
            IRepository<Partner, long> partnerRepository,
            IRepository<Product, long> productRepository)
        {
            _binaryObjectManager = binaryObjectManager;
            _userManager = userManager;
            _clientRepository = clientRepository;
            _agentRepository = agentRepository;
            _partnerRepository = partnerRepository;
            _productRepository = productRepository;
        }

        public async Task<string> GetProfilePictureContentForUser(UserIdentifier userIdentifier)
        {
            var user = await _userManager.GetUserOrNullAsync(userIdentifier);
            if (user?.ProfilePictureId == null)
            {
                return "";
            }
             
            var file = await _binaryObjectManager.GetOrNullAsync(user.ProfilePictureId.Value);
            return file == null ? "" : Convert.ToBase64String(file.Bytes);
        }
        public async Task<string> GetProfilePictureContentForClient(long clientId)
       {
            var client = await _clientRepository.GetAsync(clientId);
            if (client?.ProfilePictureId == null)
            {
                return "";
            }
             
            var file = await _binaryObjectManager.GetOrNullAsync(client.ProfilePictureId.Value);
            return file == null ? "" : Convert.ToBase64String(file.Bytes);
        }

        public async Task<string> GetProfilePictureContentForAgent(long agentId)
        {
            var agent = await _agentRepository.GetAsync(agentId);
            if (agent?.ProfilePictureId == null)
            {
                return "";
            }
             
            var file = await _binaryObjectManager.GetOrNullAsync(agent.ProfilePictureId.Value);
            return file == null ? "" : Convert.ToBase64String(file.Bytes);
        }
        

        public async Task<string> GetProfilePictureContentForPartner(long partnerId)
        {
            var partner = await _partnerRepository.GetAsync(partnerId);
            if (partner?.ProfilePictureId == null)
            {
                return "";
            }
             
            var file = await _binaryObjectManager.GetOrNullAsync(partner.ProfilePictureId.Value);
            return file == null ? "" : Convert.ToBase64String(file.Bytes);
        }
        

        public async Task<string> GetProfilePictureContentForProduct(long productId)
        {
            var product = await _productRepository.GetAsync(productId);
            if (product?.ProfilePictureId == null)
            {
                return "";
            }
             
            var file = await _binaryObjectManager.GetOrNullAsync(product.ProfilePictureId.Value);
            return file == null ? "" : Convert.ToBase64String(file.Bytes);
        }

        public async Task<string> GetProfilePictureContent(Guid pictureId)
        { 
            var file = await _binaryObjectManager.GetOrNullAsync(pictureId);
            return file == null ? "" : Convert.ToBase64String(file.Bytes);
        } 
    }
}