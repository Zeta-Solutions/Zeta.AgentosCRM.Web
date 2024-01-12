using Abp.Application.Services.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using System;
using System.Threading.Tasks;
using Zeta.AgentosCRM.Tenants.Email;
using Zeta.AgentosCRM.Tenants.Email.Dtos;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Email;
using Zeta.AgentosCRM.Web.Controllers;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Controllers
{
    [Area("AppAreaName")]
    public class SentEmailController : AgentosCRMControllerBase
    {
        private readonly ISentEmailsAppService _sentEmailsAppService;
        public SentEmailController(ISentEmailsAppService sentEmailsAppService)
        {
            _sentEmailsAppService = sentEmailsAppService;
        }
        public IActionResult Index()
        {
            var Model = new SentEmailsViewModel
            {
                FilterText = ""
            };
            return View(Model);
        }
        public async Task<PartialViewResult> ViewSentEmails(int id)
        {
            var getSentEmailForViewDto = await _sentEmailsAppService.GetSentEmailForView(id);
            var model = new SentEmailViewModel()
            {
                SentEmail = getSentEmailForViewDto.SentEmail,
            };
            return PartialView("_ViewSubjectAreaModal", model);
        }
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetSentEmailForEditOutput getSentEmailForEditOutput;
            if (id.HasValue)
            {
                getSentEmailForEditOutput = await _sentEmailsAppService.GetSentEmailForEdit(new EntityDto<long> { Id = (long)id });
            }
            else
            {
                getSentEmailForEditOutput = new GetSentEmailForEditOutput
                {
                    SentEmail = new CreateOrEditSentEmailDto()
                };
            }
            var viewModel = new CreateOrEditEmailModelViewModel()
            {
                SentEmail = getSentEmailForEditOutput.SentEmail,
                SentEmailEmailConfigurationList = await _sentEmailsAppService.GetAllEmailConfigurationForTableDropdown(),
                SentEmailClientList = await _sentEmailsAppService.GetAllClientForTableDropdown(),
                SentEmailApplicationList = await _sentEmailsAppService.GetAllApplicationForTableDropdown(),
                SentEmailEmailTemplateList = await _sentEmailsAppService.GetAllEmailTemplateForTableDropdown(),

            };
            return PartialView("_CreateOrEditModal", viewModel);
        }
        //public static void SentEmail([FromBody] string[] args)
        //{
        //    MailAddress to = new MailAddress("ToAddress");
        //    MailAddress from = new MailAddress("FromAddress");

        //    MailMessage email = new MailMessage(from, to);
        //    email.Subject = "Testing out email sending";
        //    email.Body = "Hello all the way from the land of C#";

        //    SmtpClient smtp = new SmtpClient();
        //    smtp.Host = "smtp.server.address";
        //    smtp.Port = 25;
        //    smtp.Credentials = new NetworkCredential("smtp_username", "smtp_password");
        //    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        //    smtp.EnableSsl = true;

        //    try
        //    {
        //        /* Send method called below is what will send off our email 
        //         * unless an exception is thrown.
        //         */
        //        smtp.Send(email);
        //    }
        //    catch (SmtpException ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //    }
        //} 
    }
}
