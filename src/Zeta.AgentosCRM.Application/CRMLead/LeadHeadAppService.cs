using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Organizations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Text;
using System.Threading.Tasks;
using Zeta.AgentosCRM.CRMLead.Dtos;
using Zeta.AgentosCRM.CRMLead.Exporting;
using Zeta.AgentosCRM.CRMSetup.LeadSource;
using Zeta.AgentosCRM.CRMClient.Dtos;
using Zeta.AgentosCRM.CRMClient.Quotation;

namespace Zeta.AgentosCRM.CRMLead
{
    public class LeadHeadAppService : AgentosCRMAppServiceBase, ILeadHeadAppService
    {
        private readonly IRepository<LeadHead, long> _leadHeadRepository;
        private readonly IRepository<OrganizationUnit, long> _lookup_organizationUnitRepository;
        private readonly IRepository<LeadSource, int> _lookup_leadSourceRepository;
        private readonly IRepository<LeadDetail, long> _leadDetailRepository;
        public LeadHeadAppService(IRepository<LeadHead, long> leadHeadRepository, IRepository<OrganizationUnit, long> lookup_organizationUnitRepository, IRepository<LeadSource, int> lookup_leadSourceRepository, IRepository<LeadDetail, long> leadDetailRepository = null)
        {
            _leadHeadRepository = leadHeadRepository;
            _lookup_organizationUnitRepository = lookup_organizationUnitRepository;
            _lookup_leadSourceRepository = lookup_leadSourceRepository;
            _leadDetailRepository = leadDetailRepository;
        }
        public async Task<PagedResultDto<GetLeadHeadForViewDto>> GetAll(GetAllLeadHeadInput input)
        {

            var filteredLeads = _leadHeadRepository.GetAll()
                        .Include(e => e.LeadSourceFk)
                        .Include(e => e.OrganizationUnitFK)
             .WhereIf(!string.IsNullOrWhiteSpace(input.FormNameFilter), e => false || e.FormName.Contains(input.FormNameFilter) ||
                      e.FormName.Contains(input.FormNameFilter));
            //.WhereIf(input.SectionName.HasValue, e => false || e.SectionName == input.SectionName.Value);

            //    var groupedLeads = filteredLeads
            //.GroupBy(e => e.FormName) // Group by FormName
            //.Select(group => group.First());

            //var pagedAndFilteredleads = filteredLeads
            //    .OrderBy(input.Sorting ?? "id asc")
            //    .PageBy(input);
            var pagedAndFilteredleads = filteredLeads
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);
            var leads = from o in filteredLeads
                        join o1 in _lookup_organizationUnitRepository.GetAll() on o.OrganizationUnitId equals o1.Id into j1
                        from s1 in j1.DefaultIfEmpty()

                        join o2 in _lookup_leadSourceRepository.GetAll() on o.LeadSourceId equals o2.Id into j2
                        from s2 in j2.DefaultIfEmpty()

                        select new
                        {

                            o.FormName,
                            o.CoverImage,
                            Id = o.Id,
                            o.Logo,
                            o.Formtittle,
                            o.HeaderNote,
                            o.TagName,
                            o.CreationTime,
                            OrganizationUnitName = s1 == null || s1.DisplayName == null ? "" : s1.DisplayName.ToString(),
                            LeadSourceName = s2 == null || s2.Name == null ? "" : s2.Name.ToString()
                        };

            var totalCount = await filteredLeads.CountAsync();

            var dbList = await leads.ToListAsync();
            var results = new List<GetLeadHeadForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetLeadHeadForViewDto()
                {
                    LeadHead = new LeadHeadDto
                    {

                        FormName = o.FormName,
                        CoverImage = o.CoverImage,
                        Id = o.Id,
                        Logo = o.Logo,
                        Formtittle = o.Formtittle,
                        HeaderNote = o.HeaderNote,
                        TagName = o.TagName,

                    },
                    LeadOrganizationUnitName = o.OrganizationUnitName,
                    LeadLeadSourceName = o.LeadSourceName,
                };

                results.Add(res);
            }

            return new PagedResultDto<GetLeadHeadForViewDto>(
                totalCount,
                results
            );

        }
        public async Task<GetLeadHeadForViewDto> GetLeadHeadForView(long id)
        {
            var lead = await _leadHeadRepository.GetAsync(id);

            var output = new GetLeadHeadForViewDto { LeadHead = ObjectMapper.Map<LeadHeadDto>(lead) };
            if (output.LeadHead.OrganizationUnitId != null)
            {
                var _lookupPartner = await _lookup_organizationUnitRepository.FirstOrDefaultAsync((int)output.LeadHead.OrganizationUnitId);
                output.LeadOrganizationUnitName = _lookupPartner?.DisplayName?.ToString();
            }

            if (output.LeadHead.LeadSourceId != null)
            {
                var _lookupPartner = await _lookup_leadSourceRepository.FirstOrDefaultAsync((int)output.LeadHead.LeadSourceId);
                output.LeadLeadSourceName = _lookupPartner?.Name?.ToString();
            }
            return output;
        }
        public async Task<GetLeadHeadForEditOutput> GetLeadHeadForEdit(EntityDto<long> input)
        {
            var Lead = await _leadHeadRepository.FirstOrDefaultAsync(input.Id);
            var LeadDeatils = await _leadDetailRepository.GetAllListAsync(p => p.LeadHeadId == input.Id);
            var output = new GetLeadHeadForEditOutput 
            {
                LeadHead = ObjectMapper.Map<CreateOrEditLeadHeadDto>(Lead),
                LeadDetail = ObjectMapper.Map <List<CreateOrEditLeadDetailDto>>(LeadDeatils) 
            };

            if (output.LeadHead.OrganizationUnitId != null)
            {
                var _lookupPartner = await _lookup_organizationUnitRepository.FirstOrDefaultAsync((int)output.LeadHead.OrganizationUnitId);
                output.LeadOrganizationUnitName = _lookupPartner?.DisplayName?.ToString();
            }

            if (output.LeadHead.LeadSourceId != null)
            {
                var _lookupPartner = await _lookup_leadSourceRepository.FirstOrDefaultAsync((int)output.LeadHead.LeadSourceId);
                output.LeadLeadSourceName = _lookupPartner?.Name?.ToString();
            }



            return output;
        }
        public async Task CreateOrEdit(CreateOrEditLeadHeadDto input)
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

        protected virtual async Task Create(CreateOrEditLeadHeadDto input)
        {
            var lead = ObjectMapper.Map<LeadHead>(input);

            if (AbpSession.TenantId != null)
            {
                lead.TenantId = (int)AbpSession.TenantId;
            }
            var LeadheadId = _leadHeadRepository.InsertAndGetIdAsync(lead).Result;
            foreach (var step in input.LeadDetails)
            {
                step.LeadheadId = LeadheadId;
                var stepEntity = ObjectMapper.Map<LeadDetail>(step);
                await _leadDetailRepository.InsertAsync(stepEntity);
            }
            CurrentUnitOfWork.SaveChanges();
            //await _leadHeadRepository.InsertAsync(lead);

        }

        protected virtual async Task Update(CreateOrEditLeadHeadDto input)
        {
            var lead = await _leadHeadRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, lead);
            foreach (var leads in input.LeadDetails)
            {

                if (leads.Id == 0)
                {
                    leads.LeadheadId = (int)input.Id;
                    var LeadDetail = ObjectMapper.Map<LeadDetail>(lead);
                    await _leadDetailRepository.InsertAsync(LeadDetail);
                }
                else
                {
                    leads.LeadheadId = (int)input.Id;
                    var workflowStep = await _leadDetailRepository.FirstOrDefaultAsync((int)leads.Id);
                    ObjectMapper.Map(leads, workflowStep);
                }
            }
        }

        public async Task Delete(EntityDto<long> input)
        {
            await _leadHeadRepository.DeleteAsync(input.Id);
        }
        public async Task<List<LeadOrganizationalUnitLookupTableDto>> GetAllOrganziationalUnitForTableDropdown()
        {
            return await _lookup_organizationUnitRepository.GetAll()
                .Select(OrganizationalUnit => new LeadOrganizationalUnitLookupTableDto
                {
                    Id = OrganizationalUnit.Id,
                    DisplayName = OrganizationalUnit == null || OrganizationalUnit.DisplayName == null ? "" : OrganizationalUnit.DisplayName.ToString()
                }).ToListAsync();
        }
        public async Task<List<LeadLeadSourceLookupTableDto>> GetAllLeadSourceForTableDropdown()
        {
            return await _lookup_leadSourceRepository.GetAll()
                .Select(leadSource => new LeadLeadSourceLookupTableDto
                {
                    Id = leadSource.Id,
                    DisplayName = leadSource == null || leadSource.Name == null ? "" : leadSource.Name.ToString()
                }).ToListAsync();
        }
    }
}
