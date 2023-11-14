using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Zeta.AgentosCRM.AttachmentTest.Dtos;
using Zeta.AgentosCRM.Dto;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Zeta.AgentosCRM.Storage;

namespace Zeta.AgentosCRM.AttachmentTest
{
    [AbpAuthorize(AppPermissions.Pages_TestAattachments)]
    public class TestAattachmentsAppService : AgentosCRMAppServiceBase, ITestAattachmentsAppService
    {
        private readonly IRepository<TestAattachment> _testAattachmentRepository;

        private readonly ITempFileCacheManager _tempFileCacheManager;
        private readonly IBinaryObjectManager _binaryObjectManager;

        public TestAattachmentsAppService(IRepository<TestAattachment> testAattachmentRepository, ITempFileCacheManager tempFileCacheManager, IBinaryObjectManager binaryObjectManager)
        {
            _testAattachmentRepository = testAattachmentRepository;

            _tempFileCacheManager = tempFileCacheManager;
            _binaryObjectManager = binaryObjectManager;

        }

        public async Task<PagedResultDto<GetTestAattachmentForViewDto>> GetAll(GetAllTestAattachmentsInput input)
        {

            var filteredTestAattachments = _testAattachmentRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Description.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter), e => e.Description.Contains(input.DescriptionFilter));

            var pagedAndFilteredTestAattachments = filteredTestAattachments
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var testAattachments = from o in pagedAndFilteredTestAattachments
                                   select new
                                   {

                                       o.Description,
                                       o.Attachment,
                                       Id = o.Id
                                   };

            var totalCount = await filteredTestAattachments.CountAsync();

            var dbList = await testAattachments.ToListAsync();
            var results = new List<GetTestAattachmentForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetTestAattachmentForViewDto()
                {
                    TestAattachment = new TestAattachmentDto
                    {

                        Description = o.Description,
                        Attachment = o.Attachment,
                        Id = o.Id,
                    }
                };
                res.TestAattachment.AttachmentFileName = await GetBinaryFileName(o.Attachment);

                results.Add(res);
            }

            return new PagedResultDto<GetTestAattachmentForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetTestAattachmentForViewDto> GetTestAattachmentForView(int id)
        {
            var testAattachment = await _testAattachmentRepository.GetAsync(id);

            var output = new GetTestAattachmentForViewDto { TestAattachment = ObjectMapper.Map<TestAattachmentDto>(testAattachment) };

            output.TestAattachment.AttachmentFileName = await GetBinaryFileName(testAattachment.Attachment);

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_TestAattachments_Edit)]
        public async Task<GetTestAattachmentForEditOutput> GetTestAattachmentForEdit(EntityDto input)
        {
            var testAattachment = await _testAattachmentRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetTestAattachmentForEditOutput { TestAattachment = ObjectMapper.Map<CreateOrEditTestAattachmentDto>(testAattachment) };

            output.AttachmentFileName = await GetBinaryFileName(testAattachment.Attachment);

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditTestAattachmentDto input)
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

        [AbpAuthorize(AppPermissions.Pages_TestAattachments_Create)]
        protected virtual async Task Create(CreateOrEditTestAattachmentDto input)
        {
            var testAattachment = ObjectMapper.Map<TestAattachment>(input);

            await _testAattachmentRepository.InsertAsync(testAattachment);
            testAattachment.Attachment = await GetBinaryObjectFromCache(input.AttachmentToken);

        }

        [AbpAuthorize(AppPermissions.Pages_TestAattachments_Edit)]
        protected virtual async Task Update(CreateOrEditTestAattachmentDto input)
        {
            var testAattachment = await _testAattachmentRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, testAattachment);
            testAattachment.Attachment = await GetBinaryObjectFromCache(input.AttachmentToken);

        }

        [AbpAuthorize(AppPermissions.Pages_TestAattachments_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _testAattachmentRepository.DeleteAsync(input.Id);
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

        [AbpAuthorize(AppPermissions.Pages_TestAattachments_Edit)]
        public async Task RemoveAttachmentFile(EntityDto input)
        {
            var testAattachment = await _testAattachmentRepository.FirstOrDefaultAsync(input.Id);
            if (testAattachment == null)
            {
                throw new UserFriendlyException(L("EntityNotFound"));
            }

            if (!testAattachment.Attachment.HasValue)
            {
                throw new UserFriendlyException(L("FileNotFound"));
            }

            await _binaryObjectManager.DeleteAsync(testAattachment.Attachment.Value);
            testAattachment.Attachment = null;
        }

    }
}