using Zeta.AgentosCRM.CRMNotes.Dtos;
using Zeta.AgentosCRM.CRMPartner.PartnerBranch;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.NotesAndTerms
{
    public class CreateOrEditNotesModalViewModel
    {
        public CreateOrEditNoteDto Note { get; set; }
        public bool IsEditMode => Note.Id.HasValue;

        public int PartnerId { get; set; }
    }
}
