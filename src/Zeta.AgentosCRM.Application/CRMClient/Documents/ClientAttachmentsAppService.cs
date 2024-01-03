using Zeta.AgentosCRM.CRMClient;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Zeta.AgentosCRM.CRMClient.Documents.Dtos;
using Zeta.AgentosCRM.Dto;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Zeta.AgentosCRM.Storage;
using Zeta.AgentosCRM.CRMClient.Profile.Dto;
using Zeta.AgentosCRM.Authorization.Users;

namespace Zeta.AgentosCRM.CRMClient.Documents
{
    [AbpAuthorize(AppPermissions.Pages_ClientAttachments)]
    public class ClientAttachmentsAppService : AgentosCRMAppServiceBase, IClientAttachmentsAppService
    {
        private const int MaxProfilePictureBytes = 5242880; //5MB
        private readonly IRepository<ClientAttachment, long> _clientAttachmentRepository;
        private readonly IRepository<Client, long> _lookup_clientRepository;

        private readonly ITempFileCacheManager _tempFileCacheManager;
        private readonly IBinaryObjectManager _binaryObjectManager;
        private readonly IRepository<User, long> _lookup_userRepository;

        public ClientAttachmentsAppService(IRepository<ClientAttachment, long> clientAttachmentRepository, IRepository<Client, long> lookup_clientRepository, ITempFileCacheManager tempFileCacheManager, IBinaryObjectManager binaryObjectManager, IRepository<User, long> lookup_userRepository = null)
        {
            _clientAttachmentRepository = clientAttachmentRepository;
            _lookup_clientRepository = lookup_clientRepository;

            _tempFileCacheManager = tempFileCacheManager;
            _binaryObjectManager = binaryObjectManager;
            _lookup_userRepository = lookup_userRepository;
        }

        public async Task<PagedResultDto<GetClientAttachmentForViewDto>> GetAll(GetAllClientAttachmentsInput input)
        {

            var filteredClientAttachments = _clientAttachmentRepository.GetAll()
                        .Include(e => e.ClientFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.Contains(input.NameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ClientFirstNameFilter), e => e.ClientFk != null && e.ClientFk.FirstName == input.ClientFirstNameFilter);

            var pagedAndFilteredClientAttachments = filteredClientAttachments
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var clientAttachments = from o in pagedAndFilteredClientAttachments
                                    join o1 in _lookup_clientRepository.GetAll() on o.ClientId equals o1.Id into j1
                                    from s1 in j1.DefaultIfEmpty()

                                    join o2 in _lookup_userRepository.GetAll() on o.CreatorUserId equals o2.Id into j2
                                    from s2 in j2.DefaultIfEmpty()
                                    select new
                                    {

                                        o.Name,
                                        o.AttachmentId,
                                        Id = o.Id,
                                        o.CreationTime,
                                        ClientFirstName = s1 == null || s1.FirstName == null ? "" : s1.FirstName.ToString(),
                                        UserName = s2 == null || s2.Name == null ? "" : s2.Name.ToString()
                                    };

            var totalCount = await filteredClientAttachments.CountAsync();

            var dbList = await clientAttachments.ToListAsync();
            var results = new List<GetClientAttachmentForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetClientAttachmentForViewDto()
                {
                    ClientAttachment = new ClientAttachmentDto
                    {

                        Name = o.Name,
                        AttachmentId = o.AttachmentId,
                        Id = o.Id,
                        CreationTime = o.CreationTime,
                    },
                    ClientFirstName = o.ClientFirstName,
                    UserName = o.UserName,
                };
               // res.ClientAttachment.AttachmentIdFileName = await GetBinaryFileName(o.AttachmentId);

                results.Add(res);
            }

            return new PagedResultDto<GetClientAttachmentForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetClientAttachmentForViewDto> GetClientAttachmentForView(long id)
        {
            var clientAttachment = await _clientAttachmentRepository.GetAsync(id);

            var output = new GetClientAttachmentForViewDto { ClientAttachment = ObjectMapper.Map<ClientAttachmentDto>(clientAttachment) };

            if (output.ClientAttachment.ClientId != null)
            {
                var _lookupClient = await _lookup_clientRepository.FirstOrDefaultAsync((long)output.ClientAttachment.ClientId);
                output.ClientFirstName = _lookupClient?.FirstName?.ToString();
            }

            output.ClientAttachment.AttachmentIdFileName = await GetBinaryFileName(clientAttachment.AttachmentId);

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_ClientAttachments_Edit)]
        public async Task<GetClientAttachmentForEditOutput> GetClientAttachmentForEdit(EntityDto<long> input)
        {
            var clientAttachment = await _clientAttachmentRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetClientAttachmentForEditOutput { ClientAttachment = ObjectMapper.Map<CreateOrEditClientAttachmentDto>(clientAttachment) };

            if (output.ClientAttachment.ClientId != null)
            {
                var _lookupClient = await _lookup_clientRepository.FirstOrDefaultAsync((long)output.ClientAttachment.ClientId);
                output.ClientFirstName = _lookupClient?.FirstName?.ToString();
            }

            output.AttachmentIdFileName = await GetBinaryFileName(clientAttachment.AttachmentId);

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditClientAttachmentDto input)
        {
            if (input.Id == null)
            {
                await Create(input);
            }
            else
            {
                await Update(input);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_ClientAttachments_Create)]
        protected virtual async Task Create(CreateOrEditClientAttachmentDto input)
        {
            var clientAttachment = ObjectMapper.Map<ClientAttachment>(input);

            if (AbpSession.TenantId != null)
            {
                clientAttachment.TenantId = (int)AbpSession.TenantId;
            }

            await _clientAttachmentRepository.InsertAsync(clientAttachment);
            //clientAttachment.AttachmentId = await GetBinaryObjectFromCache(input.AttachmentIdToken);

        }

        [AbpAuthorize(AppPermissions.Pages_ClientAttachments_Edit)]
        protected virtual async Task Update(CreateOrEditClientAttachmentDto input)
        {
            var clientAttachment = await _clientAttachmentRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, clientAttachment);
            //clientAttachment.AttachmentId = await GetBinaryObjectFromCache(input.AttachmentIdToken);

        }

        [AbpAuthorize(AppPermissions.Pages_ClientAttachments_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _clientAttachmentRepository.DeleteAsync(input.Id);
        }
        [AbpAuthorize(AppPermissions.Pages_ClientAttachments)]
        public async Task<List<ClientAttachmentClientLookupTableDto>> GetAllClientForTableDropdown()
        {
            return await _lookup_clientRepository.GetAll()
                .Select(client => new ClientAttachmentClientLookupTableDto
                {
                    Id = client.Id,
                    DisplayName = client == null || client.FirstName == null ? "" : client.FirstName.ToString()
                }).ToListAsync();
        }

        private async Task<Guid?> GetBinaryObjectFromCache(string fileToken)
        {
            if (fileToken.IsNullOrWhiteSpace())
            {
                return null;
            }

            var fileCache = _tempFileCacheManager.GetFileInfo(fileToken);

            if (fileCache == null)
            {
                throw new UserFriendlyException("There is no such file with the token: " + fileToken);
            }

            var storedFile = new BinaryObject(AbpSession.TenantId, fileCache.File, fileCache.FileName);
            await _binaryObjectManager.SaveAsync(storedFile);

            return storedFile.Id;
        }

        private async Task<string> GetBinaryFileName(Guid? fileId)
        {
            if (!fileId.HasValue)
            {
                return null;
            }

            var file = await _binaryObjectManager.GetOrNullAsync(fileId.Value);
            return file?.Description;
        }

        [AbpAuthorize(AppPermissions.Pages_ClientAttachments_Edit)]
        public async Task RemoveAttachmentIdFile(EntityDto<long> input)
        {
            var clientAttachment = await _clientAttachmentRepository.FirstOrDefaultAsync(input.Id);
            if (clientAttachment == null)
            {
                throw new UserFriendlyException(L("EntityNotFound"));
            }

            if (!clientAttachment.AttachmentId.HasValue)
            {
                throw new UserFriendlyException(L("FileNotFound"));
            }

            await _binaryObjectManager.DeleteAsync(clientAttachment.AttachmentId.Value);
            clientAttachment.AttachmentId = null;
            await _clientAttachmentRepository.DeleteAsync(input.Id);
        }


        public async Task<Guid> InsertAttachmentForClient(UpdateClientProfilePictureInput input)
        {

            //var logoFile = Request.Form.Files.First();
            byte[] byteArray;

            var imageBytes = _tempFileCacheManager.GetFile(input.FileToken);

            if (imageBytes == null)
            {
                throw new UserFriendlyException("There is no such file with the token: " + input.FileToken);
            }

            byteArray = imageBytes;

            if (byteArray.Length > MaxProfilePictureBytes)
            {
                throw new UserFriendlyException(L("ResizedProfilePicture_Warn_SizeLimit",
                    AppConsts.ResizedMaxProfilePictureBytesUserFriendlyValue));
            }

            var storedFile = new BinaryObject(AbpSession.TenantId, byteArray,
                $"Attachment of Client {DateTime.UtcNow}");
            await _binaryObjectManager.SaveAsync(storedFile);

            return storedFile.Id;
        }

    }
}