using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zeta.AgentosCRM.CRMLead.Dtos;
using Microsoft.EntityFrameworkCore;
using Zeta.AgentosCRM.CRMClient.Dtos;
using Zeta.AgentosCRM.Dto;
using Zeta.AgentosCRM.CRMClient.Exporting;
using Zeta.AgentosCRM.CRMLead.Exporting;

namespace Zeta.AgentosCRM.CRMLead
{
    public class LeadDetailAppService : AgentosCRMAppServiceBase, ILeadDetailAppService
    {
        private readonly IRepository<LeadDetail, long> _leadDetailRepository;
        private readonly ILeadDetailsExcelExporter _leadDetailsExcelExporter;
        public LeadDetailAppService(IRepository<LeadDetail, long> leadDetailRepository)
        {
            _leadDetailRepository = leadDetailRepository;
        }
        public async Task<PagedResultDto<GetLeadDetailForViewDto>> GetAll(GetAllLeadDetailsInput input)
        {

            var filteredLeads = _leadDetailRepository.GetAll()

           .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.PropertyName.Contains(input.Filter) || e.PropertyName.Contains(input.Filter) || e.PropertyName.Contains(input.Filter));
            var pagedAndFilteredEmailTemplates = filteredLeads
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var leaddetail = from o in filteredLeads

                        select new
                        {

                            o.PropertyName,
                            o.Inputtype,
                            Id = o.Id,
                            o.Status,
                            o.SectionName,
                            o.CreationTime,                          
                        };

            var totalCount = await filteredLeads.CountAsync();

            var dbList = await leaddetail.ToListAsync();
            var results = new List<GetLeadDetailForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetLeadDetailForViewDto()
                {
                    LeadDetail = new LeadDetailDto
                    {

                        PropertyName = o.PropertyName,
                        Inputtype = o.Inputtype,
                        Id = o.Id,                        
                        Status = o.Status,
                        SectionName = o.SectionName,

                    },
                };

                results.Add(res);
            }

            return new PagedResultDto<GetLeadDetailForViewDto>(
                totalCount,
                results
            );

        }
        public async Task<GetLeadDetailForViewDto> GetLeadDetailForView(long id)
        {
            var lead = await _leadDetailRepository.GetAsync(id);

            var output = new GetLeadDetailForViewDto { LeadDetail = ObjectMapper.Map<LeadDetailDto>(lead) };
            return output;
        }
        public async Task<GetLeadDetailForEditOutput> GetLeadDetailForEdit(EntityDto<long> input)
        {
            var Lead = await _leadDetailRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetLeadDetailForEditOutput { LeadDetail= ObjectMapper.Map<CreateOrEditLeadDetailDto>(Lead) };
            return output;
        }
        public async Task CreateOrEdit(CreateOrEditLeadDetailDto input)
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
        protected virtual async Task Create(CreateOrEditLeadDetailDto input)
        {
            var lead = ObjectMapper.Map<LeadDetail>(input);

            if (AbpSession.TenantId != null)
            {
                lead.TenantId = (int)AbpSession.TenantId;
            }

            await _leadDetailRepository.InsertAsync(lead);

        }

        protected virtual async Task Update(CreateOrEditLeadDetailDto input)
        {
            var lead = await _leadDetailRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, lead);

        }

        public async Task Delete(EntityDto<long> input)
        {
            await _leadDetailRepository.DeleteAsync(input.Id);
        }
        public async Task<FileDto> GetLeadDetailsToExcel(GetAllLeadDetailsForExcelInput input)
        {

            var filteredClients = _leadDetailRepository.GetAll()

           .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.PropertyName.Contains(input.Filter) || e.PropertyName.Contains(input.Filter) || e.PropertyName.Contains(input.Filter));


            var query = (from o in filteredClients
                         

                         select new GetLeadDetailForViewDto()
                         {
                             LeadDetail = new LeadDetailDto
                             {
                                 PropertyName = o.PropertyName,
                                 Inputtype = o.Inputtype,
                                 Id = o.Id,
                                 Status = o.Status,
                                 SectionName = o.SectionName,
                             },
                            
                         });

            var LeadDetailListDtos = await query.ToListAsync();

            return _leadDetailsExcelExporter.ExportToFile(LeadDetailListDtos);
        }
    }
}
