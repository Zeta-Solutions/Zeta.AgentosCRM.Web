using Abp.Application.Services.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Zeta.AgentosCRM.CRMClient.Quotation;
using Zeta.AgentosCRM.CRMClient.Quotation.Dtos;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Clients;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Clients.ClientQuotations;
using Zeta.AgentosCRM.Web.Controllers;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Controllers
{
    [Area("AppAreaName")]
    public class QuotationController : AgentosCRMControllerBase
    {
        private readonly IClientQuotationHeadsAppService _clientQuotationHeadsAppService;
        private readonly IClientQuotationDetailsAppService _clientQuotationDetailsAppService;

        public QuotationController(IClientQuotationHeadsAppService clientQuotationHeadsAppService, IClientQuotationDetailsAppService clientQuotationDetailsAppService)
        {
            _clientQuotationHeadsAppService = clientQuotationHeadsAppService;
            _clientQuotationDetailsAppService = clientQuotationDetailsAppService;
        }

        #region "Client Quotation"
        public ActionResult Index()
        {
            var model = new ClientQuotationsViewModel
            {
                FilterText = ""
            };

            return View(model);
        }
        public async Task<ActionResult> ClientQuotationIndex(long? id)
        {

            long applicationId = id.GetValueOrDefault();
            var getClientQuotationHeadForViewDto = await _clientQuotationHeadsAppService.GetClientQuotationHeadForView(applicationId);
            var model = new ClientQuotationViewModel()
            {
                ClientQuotationHead = getClientQuotationHeadForViewDto.ClientQuotationHead

            };

            return View("~/ClientsQuotation/ClientQuotationIndex.cshtml", model);
        }
        public async Task<ActionResult> CreateOrEditClientQuotationModal(long? id)
        {
            GetClientQuotationHeadForEditOutput getClientQuotationHeadForEditOutput;
            if (id.HasValue)
            {
                getClientQuotationHeadForEditOutput = await _clientQuotationHeadsAppService.GetClientQuotationHeadForEdit(new EntityDto<long> { Id = (long)id });
            }
            else
            {
                getClientQuotationHeadForEditOutput = new GetClientQuotationHeadForEditOutput
                {
                    ClientQuotationHead = new CreateOrEditClientQuotationHeadDto()
                };
                getClientQuotationHeadForEditOutput.ClientQuotationHead.DueDate = DateTime.Now;
            }
            var viewModel = new CreateOrEditClientQuotationsViewModel()
            {
                ClientQuotationHead = getClientQuotationHeadForEditOutput.ClientQuotationHead,
                QuotationHeadClientList = await _clientQuotationHeadsAppService.GetAllClientForTableDropdown(),
                QuotationHeadCRMCurrencyList = await _clientQuotationHeadsAppService.GetAllCRMCurrencyForTableDropdown(),

            };

            return View("ClientsQuotation/ClientQuotationsDetail", viewModel);
        }
        public ActionResult CreateClientQuotationModal(long? id)
        {
            return PartialView("_CreateClientQuotationModal");
        }
        public async Task<PartialViewResult> CreateOrEditQuotationDetailModal(long? id)
        {
            GetClientQuotationDetailForEditOutput getClientQuotationDetailForEditOutput;
            if (id.HasValue)
            {
                getClientQuotationDetailForEditOutput = await _clientQuotationDetailsAppService.GetClientQuotationDetailForEdit(new EntityDto<long> { Id = (long)id });
            }
            else
            {
                getClientQuotationDetailForEditOutput = new GetClientQuotationDetailForEditOutput
                {
                    ClientQuotationDetail = new CreateOrEditClientQuotationDetailDto()
                };
                //getCRMTaskForEditOutput.CRMTask.DueDate = DateTime.Now;
                //getCRMTaskForEditOutput.CRMTask.DueTime = DateTime.Now;
            }

            var viewModel = new CreateOrEditQuotationDetailViewModel()
            {
                ClientQuotationDetail = getClientQuotationDetailForEditOutput.ClientQuotationDetail,
                ProductName = getClientQuotationDetailForEditOutput.ProductName,
                BranchName = getClientQuotationDetailForEditOutput.BranchName,
                PartnerName = getClientQuotationDetailForEditOutput.PartnerPartnerName,
                WorkflowName = getClientQuotationDetailForEditOutput.WorkflowName,
                ClientQuotationDetailWorkflowList = await _clientQuotationDetailsAppService.GetAllWorkflowForTableDropdown(),
                ClientQuotationDetailPartnerList = await _clientQuotationDetailsAppService.GetAllPartnerForTableDropdown(),
                ClientQuotationDetailBranchList = await _clientQuotationDetailsAppService.GetAllBranchForTableDropdown(),
                ClientQuotationDetailProducList = await _clientQuotationDetailsAppService.GetAllProductForTableDropdown(),


            };
            return PartialView("ClientsQuotation/_CreateOrEditQuotationDetailModal", viewModel);

        }
        public async Task<ActionResult> ClientsQuotationPreview(long? id)
        {
            GetClientQuotationHeadForEditOutput getClientQuotationHeadForEditOutput;
            if (id.HasValue)
            {
                getClientQuotationHeadForEditOutput = await _clientQuotationHeadsAppService.GetClientQuotationHeadForEdit(new EntityDto<long> { Id = (long)id });
            }
            else
            {
                getClientQuotationHeadForEditOutput = new GetClientQuotationHeadForEditOutput
                {
                    ClientQuotationHead = new CreateOrEditClientQuotationHeadDto()
                };
                getClientQuotationHeadForEditOutput.ClientQuotationHead.DueDate = DateTime.Now;
            }
            var viewModel = new CreateOrEditClientQuotationsViewModel()
            {
                ClientQuotationHead = getClientQuotationHeadForEditOutput.ClientQuotationHead,
                QuotationHeadClientList = await _clientQuotationHeadsAppService.GetAllClientForTableDropdown(),
                QuotationHeadCRMCurrencyList = await _clientQuotationHeadsAppService.GetAllCRMCurrencyForTableDropdown(),
                clientQuotationDeatils = getClientQuotationHeadForEditOutput.ClientQuotationDetail,
            };
            return View("ClientsQuotation/QuotationPreview", viewModel);
        }
        #endregion
    }
}
