using Zeta.AgentosCRM.CRMClient;
using Zeta.AgentosCRM.CRMPartner;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Zeta.AgentosCRM.CRMNotes.Exporting;
using Zeta.AgentosCRM.CRMNotes.Dtos;
using Zeta.AgentosCRM.Dto;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Zeta.AgentosCRM.Storage;

namespace Zeta.AgentosCRM.CRMNotes
{
    [AbpAuthorize(AppPermissions.Pages_Notes)]
    public class NotesAppService : AgentosCRMAppServiceBase, INotesAppService
    {
        private readonly IRepository<Note, long> _noteRepository;
        private readonly INotesExcelExporter _notesExcelExporter;
        private readonly IRepository<Client, long> _lookup_clientRepository;
        private readonly IRepository<Partner, long> _lookup_partnerRepository;

        public NotesAppService(IRepository<Note, long> noteRepository, INotesExcelExporter notesExcelExporter, IRepository<Client, long> lookup_clientRepository, IRepository<Partner, long> lookup_partnerRepository)
        {
            _noteRepository = noteRepository;
            _notesExcelExporter = notesExcelExporter;
            _lookup_clientRepository = lookup_clientRepository;
            _lookup_partnerRepository = lookup_partnerRepository;

        }

        public async Task<PagedResultDto<GetNoteForViewDto>> GetAll(GetAllNotesInput input)
        {

            var filteredNotes = _noteRepository.GetAll()
                        .Include(e => e.ClientFk)
                        .Include(e => e.PartnerFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Title.Contains(input.Filter) || e.Description.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TitleFilter), e => e.Title.Contains(input.TitleFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter), e => e.Description.Contains(input.DescriptionFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ClientDisplayPropertyFilter), e => string.Format("{0} {1}", e.ClientFk == null || e.ClientFk.FirstName == null ? "" : e.ClientFk.FirstName.ToString()
, e.ClientFk == null || e.ClientFk.LastName == null ? "" : e.ClientFk.LastName.ToString()
) == input.ClientDisplayPropertyFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PartnerPartnerNameFilter), e => e.PartnerFk != null && e.PartnerFk.PartnerName == input.PartnerPartnerNameFilter);

            var pagedAndFilteredNotes = filteredNotes
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var notes = from o in pagedAndFilteredNotes
                        join o1 in _lookup_clientRepository.GetAll() on o.ClientId equals o1.Id into j1
                        from s1 in j1.DefaultIfEmpty()

                        join o2 in _lookup_partnerRepository.GetAll() on o.PartnerId equals o2.Id into j2
                        from s2 in j2.DefaultIfEmpty()

                        select new
                        {

                            o.Title,
                            o.Description,
                            Id = o.Id,
                            ClientDisplayProperty = string.Format("{0} {1}", s1 == null || s1.FirstName == null ? "" : s1.FirstName.ToString()
        , s1 == null || s1.LastName == null ? "" : s1.LastName.ToString()
        ),
                            PartnerPartnerName = s2 == null || s2.PartnerName == null ? "" : s2.PartnerName.ToString()
                        };

            var totalCount = await filteredNotes.CountAsync();

            var dbList = await notes.ToListAsync();
            var results = new List<GetNoteForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetNoteForViewDto()
                {
                    Note = new NoteDto
                    {

                        Title = o.Title,
                        Description = o.Description,
                        Id = o.Id,
                    },
                    ClientDisplayProperty = o.ClientDisplayProperty,
                    PartnerPartnerName = o.PartnerPartnerName
                };

                results.Add(res);
            }

            return new PagedResultDto<GetNoteForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetNoteForViewDto> GetNoteForView(long id)
        {
            var note = await _noteRepository.GetAsync(id);

            var output = new GetNoteForViewDto { Note = ObjectMapper.Map<NoteDto>(note) };

            if (output.Note.ClientId != null)
            {
                var _lookupClient = await _lookup_clientRepository.FirstOrDefaultAsync((long)output.Note.ClientId);
                output.ClientDisplayProperty = string.Format("{0} {1}", _lookupClient.FirstName, _lookupClient.LastName);
            }

            if (output.Note.PartnerId != null)
            {
                var _lookupPartner = await _lookup_partnerRepository.FirstOrDefaultAsync((long)output.Note.PartnerId);
                output.PartnerPartnerName = _lookupPartner?.PartnerName?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Notes_Edit)]
        public async Task<GetNoteForEditOutput> GetNoteForEdit(EntityDto<long> input)
        {
            var note = await _noteRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetNoteForEditOutput { Note = ObjectMapper.Map<CreateOrEditNoteDto>(note) };

            if (output.Note.ClientId != null)
            {
                var _lookupClient = await _lookup_clientRepository.FirstOrDefaultAsync((long)output.Note.ClientId);
                output.ClientDisplayProperty = string.Format("{0} {1}", _lookupClient.FirstName, _lookupClient.LastName);
            }

            if (output.Note.PartnerId != null)
            {
                var _lookupPartner = await _lookup_partnerRepository.FirstOrDefaultAsync((long)output.Note.PartnerId);
                output.PartnerPartnerName = _lookupPartner?.PartnerName?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditNoteDto input)
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

        [AbpAuthorize(AppPermissions.Pages_Notes_Create)]
        protected virtual async Task Create(CreateOrEditNoteDto input)
        {
            var note = ObjectMapper.Map<Note>(input);

            if (AbpSession.TenantId != null)
            {
                note.TenantId = (int)AbpSession.TenantId;
            }

            await _noteRepository.InsertAsync(note);

        }

        [AbpAuthorize(AppPermissions.Pages_Notes_Edit)]
        protected virtual async Task Update(CreateOrEditNoteDto input)
        {
            var note = await _noteRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, note);

        }

        [AbpAuthorize(AppPermissions.Pages_Notes_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _noteRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetNotesToExcel(GetAllNotesForExcelInput input)
        {

            var filteredNotes = _noteRepository.GetAll()
                        .Include(e => e.ClientFk)
                        .Include(e => e.PartnerFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Title.Contains(input.Filter) || e.Description.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TitleFilter), e => e.Title.Contains(input.TitleFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter), e => e.Description.Contains(input.DescriptionFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ClientDisplayPropertyFilter), e => string.Format("{0} {1}", e.ClientFk == null || e.ClientFk.FirstName == null ? "" : e.ClientFk.FirstName.ToString()
, e.ClientFk == null || e.ClientFk.LastName == null ? "" : e.ClientFk.LastName.ToString()
) == input.ClientDisplayPropertyFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PartnerPartnerNameFilter), e => e.PartnerFk != null && e.PartnerFk.PartnerName == input.PartnerPartnerNameFilter);

            var query = (from o in filteredNotes
                         join o1 in _lookup_clientRepository.GetAll() on o.ClientId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()

                         join o2 in _lookup_partnerRepository.GetAll() on o.PartnerId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()

                         select new GetNoteForViewDto()
                         {
                             Note = new NoteDto
                             {
                                 Title = o.Title,
                                 Description = o.Description,
                                 Id = o.Id
                             },
                             ClientDisplayProperty = string.Format("{0} {1}", s1 == null || s1.FirstName == null ? "" : s1.FirstName.ToString()
, s1 == null || s1.LastName == null ? "" : s1.LastName.ToString()
),
                             PartnerPartnerName = s2 == null || s2.PartnerName == null ? "" : s2.PartnerName.ToString()
                         });

            var noteListDtos = await query.ToListAsync();

            return _notesExcelExporter.ExportToFile(noteListDtos);
        }

        [AbpAuthorize(AppPermissions.Pages_Notes)]
        public async Task<List<NoteClientLookupTableDto>> GetAllClientForTableDropdown()
        {
            return await _lookup_clientRepository.GetAll()
                .Select(client => new NoteClientLookupTableDto
                {
                    Id = client.Id,
                    DisplayName = string.Format("{0} {1}", client.FirstName, client.LastName)
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_Notes)]
        public async Task<List<NotePartnerLookupTableDto>> GetAllPartnerForTableDropdown()
        {
            return await _lookup_partnerRepository.GetAll()
                .Select(partner => new NotePartnerLookupTableDto
                {
                    Id = partner.Id,
                    DisplayName = partner == null || partner.PartnerName == null ? "" : partner.PartnerName.ToString()
                }).ToListAsync();
        }

    }
}