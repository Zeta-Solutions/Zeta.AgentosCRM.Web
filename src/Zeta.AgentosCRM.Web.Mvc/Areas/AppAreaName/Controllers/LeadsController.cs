using Abp.Application.Services.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Zeta.AgentosCRM.CRMLead;
using Zeta.AgentosCRM.CRMLead.Dtos;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.CRMLead;
using Zeta.AgentosCRM.Web.Controllers;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Controllers
{
    [Area("AppAreaName")]
    public class LeadsController : AgentosCRMControllerBase
    {
        private readonly ILeadHeadAppService _leadHeadAppService;
        private readonly ILeadDetailAppService _leadDetailAppService;
        public LeadsController(ILeadHeadAppService leadHeadAppService, ILeadDetailAppService leadDetailAppService)
        {
            _leadHeadAppService = leadHeadAppService;
            _leadDetailAppService = leadDetailAppService;
        }


        public ActionResult Index()
        {
            var model = new LeadHeadsViewModel
            {
                FilterText = ""
            };

            return View(model);
        }
        public ActionResult CreateLead(string? FormName)
        {
            ViewBag.theFormName = "LeadForm";
            return View();
        }
        public ActionResult LeadForm()
        {

            return View();
        }
        public async Task<ActionResult> Leads(int id)
        {
            var getLeadForViewDto = await _leadHeadAppService.GetLeadHeadForView(id);
            var model = new LeadHeadViewModel()
            {
                LeadHead = getLeadForViewDto.LeadHead


            };

            return View("NotesAndTerms/NotesAndTerms", model);
        }
        public async Task<ActionResult> LeadAllFields(long? id)
        {
            GetLeadHeadForEditOutput getLeadsForEditOutput;
            if (id.HasValue)
            {
                getLeadsForEditOutput = await _leadHeadAppService.GetLeadHeadForEdit(new EntityDto<long> { Id = (long)id });
            }
            else
            {
                getLeadsForEditOutput = new GetLeadHeadForEditOutput
                {
                    LeadHead = new CreateOrEditLeadHeadDto()
                };
            }
            var viewModel = new CreateOrEditLeadHeadModelViewModel()
            {
                LeadHead = getLeadsForEditOutput.LeadHead,
                LeadOrganizationalList = await _leadHeadAppService.GetAllOrganziationalUnitForTableDropdown(),
                LeadLeadSourceList = await _leadHeadAppService.GetAllLeadSourceForTableDropdown(),

            };

            //return PartialView("_CreateOrEditNotesModal", viewModel);
            return View(viewModel);
        }
    }
}
