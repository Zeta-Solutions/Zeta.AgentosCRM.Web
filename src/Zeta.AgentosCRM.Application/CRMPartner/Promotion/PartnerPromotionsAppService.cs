using Zeta.AgentosCRM.CRMPartner;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Zeta.AgentosCRM.CRMPartner.Promotion.Dtos;
using Zeta.AgentosCRM.Dto;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Zeta.AgentosCRM.Storage;
using Zeta.AgentosCRM.CRMSetup.Dtos;
using Zeta.AgentosCRM.CRMSetup;
using Stripe;
using Microsoft.AspNetCore.Mvc;

namespace Zeta.AgentosCRM.CRMPartner.Promotion
{
    [AbpAuthorize(AppPermissions.Pages_PartnerPromotions)]
    public class PartnerPromotionsAppService : AgentosCRMAppServiceBase, IPartnerPromotionsAppService
    {
        private readonly IRepository<PartnerPromotion, long> _partnerPromotionRepository;
        private readonly IRepository<Partner, long> _lookup_partnerRepository;
        private readonly IRepository<PromotionProduct,long> _promotionProductRepository;

        private readonly ITempFileCacheManager _tempFileCacheManager;
        private readonly IBinaryObjectManager _binaryObjectManager;

        public PartnerPromotionsAppService(IRepository<PartnerPromotion, long> partnerPromotionRepository, IRepository<Partner, long> lookup_partnerRepository, ITempFileCacheManager tempFileCacheManager, IBinaryObjectManager binaryObjectManager, IRepository<PromotionProduct,long> promotionProductRepository)
        {
            _partnerPromotionRepository = partnerPromotionRepository;
            _lookup_partnerRepository = lookup_partnerRepository;

            _tempFileCacheManager = tempFileCacheManager;
            _binaryObjectManager = binaryObjectManager;
            _promotionProductRepository = promotionProductRepository;
        }

        public async Task<PagedResultDto<GetPartnerPromotionForViewDto>> GetAll(GetAllPartnerPromotionsInput input)
        {

            var filteredPartnerPromotions = _partnerPromotionRepository.GetAll()
                        .Include(e => e.PartnerFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter) || e.Description.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.Contains(input.NameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter), e => e.Description.Contains(input.DescriptionFilter))
                        .WhereIf(input.MinStartDateFilter != null, e => e.StartDate >= input.MinStartDateFilter)
                        .WhereIf(input.MaxStartDateFilter != null, e => e.StartDate <= input.MaxStartDateFilter)
                        .WhereIf(input.MinExpiryDateFilter != null, e => e.ExpiryDate >= input.MinExpiryDateFilter)
                        .WhereIf(input.MaxExpiryDateFilter != null, e => e.ExpiryDate <= input.MaxExpiryDateFilter)
                        .WhereIf(input.ApplyToFilter.HasValue && input.ApplyToFilter > -1, e => (input.ApplyToFilter == 1 && e.ApplyTo) || (input.ApplyToFilter == 0 && !e.ApplyTo))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PartnerPartnerNameFilter), e => e.PartnerFk != null && e.PartnerFk.PartnerName == input.PartnerPartnerNameFilter)
             .WhereIf(input.PartnerIdFilter.HasValue, e => false || e.PartnerId == input.PartnerIdFilter.Value);
            var pagedAndFilteredPartnerPromotions = filteredPartnerPromotions
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var partnerPromotions = from o in pagedAndFilteredPartnerPromotions
                                    join o1 in _lookup_partnerRepository.GetAll() on o.PartnerId equals o1.Id into j1
                                    from s1 in j1.DefaultIfEmpty()

                                    select new
                                    {

                                        o.Name,
                                        o.Description,
                                        o.Attachment,
                                        o.StartDate,
                                        o.ExpiryDate,
                                        o.ApplyTo,
                                        Id = o.Id,
                                        PartnerPartnerName = s1 == null || s1.PartnerName == null ? "" : s1.PartnerName.ToString()
                                    };

            var totalCount = await filteredPartnerPromotions.CountAsync();

            var dbList = await partnerPromotions.ToListAsync();
            var results = new List<GetPartnerPromotionForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetPartnerPromotionForViewDto()
                {
                    PartnerPromotion = new PartnerPromotionDto
                    {

                        Name = o.Name,
                        Description = o.Description,
                        Attachment = o.Attachment,
                        StartDate = o.StartDate,
                        ExpiryDate = o.ExpiryDate,
                        ApplyTo = o.ApplyTo,
                        Id = o.Id,
                    },
                    PartnerPartnerName = o.PartnerPartnerName
                };
                res.PartnerPromotion.AttachmentFileName = await GetBinaryFileName(o.Attachment);

                results.Add(res);
            }

            return new PagedResultDto<GetPartnerPromotionForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetPartnerPromotionForViewDto> GetPartnerPromotionForView(long id)
        {
            var partnerPromotion = await _partnerPromotionRepository.GetAsync(id);

            var output = new GetPartnerPromotionForViewDto { PartnerPromotion = ObjectMapper.Map<PartnerPromotionDto>(partnerPromotion) };

            if (output.PartnerPromotion.PartnerId != null)
            {
                var _lookupPartner = await _lookup_partnerRepository.FirstOrDefaultAsync((long)output.PartnerPromotion.PartnerId);
                output.PartnerPartnerName = _lookupPartner?.PartnerName?.ToString();
            }

            output.PartnerPromotion.AttachmentFileName = await GetBinaryFileName(partnerPromotion.Attachment);

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_PartnerPromotions_Edit)]
        public async Task<GetPartnerPromotionForEditOutput> GetPartnerPromotionForEdit(EntityDto<long> input)
        {
            var partnerPromotion = await _partnerPromotionRepository.FirstOrDefaultAsync(input.Id);
            var promotionProduct = await _promotionProductRepository.GetAllListAsync(p => p.PartnerPromotionId == input.Id );

            var output = new GetPartnerPromotionForEditOutput
            {
                PartnerPromotion = ObjectMapper.Map<CreateOrEditPartnerPromotionDto>(partnerPromotion),
                PromotionProduct = ObjectMapper.Map<List<CreateOrEditPromotionProductDto>>(promotionProduct)
            };

            if (output.PartnerPromotion.PartnerId != null)
            {
                var _lookupPartner = await _lookup_partnerRepository.FirstOrDefaultAsync((long)output.PartnerPromotion.PartnerId);
                output.PartnerPartnerName = _lookupPartner?.PartnerName?.ToString();
            }

            output.AttachmentFileName = await GetBinaryFileName(partnerPromotion.Attachment);

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditPartnerPromotionDto input)
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

        [AbpAuthorize(AppPermissions.Pages_PartnerPromotions_Create)]
        protected virtual async Task Create([FromBody] CreateOrEditPartnerPromotionDto input)
        {
            var partnerPromotion = ObjectMapper.Map<PartnerPromotion>(input);

            if (AbpSession.TenantId != null)
            {
                partnerPromotion.TenantId = (int)AbpSession.TenantId;
            }
            var partnerPromotionId = _partnerPromotionRepository.InsertAndGetIdAsync(partnerPromotion).Result;
            foreach (var step in input.Steps)
            {
                step.PartnerPromotionId = partnerPromotionId;
                var stepEntity = ObjectMapper.Map<PromotionProduct>(step);
                await _promotionProductRepository.InsertAsync(stepEntity);
            }
            CurrentUnitOfWork.SaveChanges();
            //await _partnerPromotionRepository.InsertAsync(partnerPromotion);
            partnerPromotion.Attachment = await GetBinaryObjectFromCache(input.AttachmentToken);

        }

        [AbpAuthorize(AppPermissions.Pages_PartnerPromotions_Edit)]
        protected virtual async Task Update(CreateOrEditPartnerPromotionDto input)
        {
            var partnerPromotion = await _partnerPromotionRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, partnerPromotion);
            partnerPromotion.Attachment = await GetBinaryObjectFromCache(input.AttachmentToken);

        }

        [AbpAuthorize(AppPermissions.Pages_PartnerPromotions_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _partnerPromotionRepository.DeleteAsync(input.Id);
        }
        [AbpAuthorize(AppPermissions.Pages_PartnerPromotions)]
        public async Task<List<PartnerPromotionPartnerLookupTableDto>> GetAllPartnerForTableDropdown()
        {
            return await _lookup_partnerRepository.GetAll()
                .Select(partner => new PartnerPromotionPartnerLookupTableDto
                {
                    Id = partner.Id,
                    DisplayName = partner == null || partner.PartnerName == null ? "" : partner.PartnerName.ToString()
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

        [AbpAuthorize(AppPermissions.Pages_PartnerPromotions_Edit)]
        public async Task RemoveAttachmentFile(EntityDto<long> input)
        {
            var partnerPromotion = await _partnerPromotionRepository.FirstOrDefaultAsync(input.Id);
            if (partnerPromotion == null)
            {
                throw new UserFriendlyException(L("EntityNotFound"));
            }

            if (!partnerPromotion.Attachment.HasValue)
            {
                throw new UserFriendlyException(L("FileNotFound"));
            }

            await _binaryObjectManager.DeleteAsync(partnerPromotion.Attachment.Value);
            partnerPromotion.Attachment = null;
        }

    }
}