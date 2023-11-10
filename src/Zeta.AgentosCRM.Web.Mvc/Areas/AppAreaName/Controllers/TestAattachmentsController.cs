using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.TestAattachments;
using Zeta.AgentosCRM.Web.Controllers;
using Zeta.AgentosCRM.Authorization;
using Zeta.AgentosCRM.AttachmentTest;
using Zeta.AgentosCRM.AttachmentTest.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;

using System.IO;
using System.Linq;
using Abp.Web.Models;
using Abp.UI;
using Abp.IO.Extensions;
using Zeta.AgentosCRM.Storage;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Controllers
{
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_TestAattachments)]
    public class TestAattachmentsController : AgentosCRMControllerBase
    {
        private readonly ITestAattachmentsAppService _testAattachmentsAppService;
        private readonly ITempFileCacheManager _tempFileCacheManager;

        private const long MaxAttachmentLength = 5242880; //5MB
        private const string MaxAttachmentLengthUserFriendlyValue = "5MB"; //5MB
        private readonly string[] AttachmentAllowedFileTypes = { "jpeg", "jpg", "png" };

        public TestAattachmentsController(ITestAattachmentsAppService testAattachmentsAppService, ITempFileCacheManager tempFileCacheManager)
        {
            _testAattachmentsAppService = testAattachmentsAppService;
            _tempFileCacheManager = tempFileCacheManager;
        }

        public ActionResult Index()
        {
            var model = new TestAattachmentsViewModel
            {
                FilterText = ""
            };

            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_TestAattachments_Create, AppPermissions.Pages_TestAattachments_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetTestAattachmentForEditOutput getTestAattachmentForEditOutput;

            if (id.HasValue)
            {
                getTestAattachmentForEditOutput = await _testAattachmentsAppService.GetTestAattachmentForEdit(new EntityDto { Id = (int)id });
            }
            else
            {
                getTestAattachmentForEditOutput = new GetTestAattachmentForEditOutput
                {
                    TestAattachment = new CreateOrEditTestAattachmentDto()
                };
            }

            var viewModel = new CreateOrEditTestAattachmentModalViewModel()
            {
                TestAattachment = getTestAattachmentForEditOutput.TestAattachment,
                AttachmentFileName = getTestAattachmentForEditOutput.AttachmentFileName,
            };

            foreach (var AttachmentAllowedFileType in AttachmentAllowedFileTypes)
            {
                viewModel.AttachmentFileAcceptedTypes += "." + AttachmentAllowedFileType + ",";
            }

            return PartialView("_CreateOrEditModal", viewModel);
        }

        public async Task<PartialViewResult> ViewTestAattachmentModal(int id)
        {
            var getTestAattachmentForViewDto = await _testAattachmentsAppService.GetTestAattachmentForView(id);

            var model = new TestAattachmentViewModel()
            {
                TestAattachment = getTestAattachmentForViewDto.TestAattachment
            };

            return PartialView("_ViewTestAattachmentModal", model);
        }

        public FileUploadCacheOutput UploadAttachmentFile()
        {
            try
            {
                //Check input
                if (Request.Form.Files.Count == 0)
                {
                    throw new UserFriendlyException(L("NoFileFoundError"));
                }

                var file = Request.Form.Files.First();
                if (file.Length > MaxAttachmentLength)
                {
                    throw new UserFriendlyException(L("Warn_File_SizeLimit", MaxAttachmentLengthUserFriendlyValue));
                }

                var fileType = Path.GetExtension(file.FileName).Substring(1);
                if (AttachmentAllowedFileTypes != null && AttachmentAllowedFileTypes.Length > 0 && !AttachmentAllowedFileTypes.Contains(fileType))
                {
                    throw new UserFriendlyException(L("FileNotInAllowedFileTypes", AttachmentAllowedFileTypes));
                }

                byte[] fileBytes;
                using (var stream = file.OpenReadStream())
                {
                    fileBytes = stream.GetAllBytes();
                }

                var fileToken = Guid.NewGuid().ToString("N");
                _tempFileCacheManager.SetFile(fileToken, new TempFileInfo(file.FileName, fileType, fileBytes));

                return new FileUploadCacheOutput(fileToken);
            }
            catch (UserFriendlyException ex)
            {
                return new FileUploadCacheOutput(new ErrorInfo(ex.Message));
            }
        }

    }
}