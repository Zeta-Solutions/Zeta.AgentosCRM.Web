using Abp.Runtime.Session;
using Abp.UI;
using Abp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using System.Threading.Tasks;
using Zeta.AgentosCRM.Graphics;
using Zeta.AgentosCRM.MultiTenancy;
using Zeta.AgentosCRM.Storage;
using Zeta.AgentosCRM.Web.Controllers;
using Abp.IO.Extensions;
using Microsoft.EntityFrameworkCore;
using Zeta.AgentosCRM.Authorization.Users.Profile;
using MimeKit;
using System.Text;
using System.IO;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Controllers
{
    [Area("AppAreaName")]
    public class DocumentAttachmentController : AgentosCRMControllerBase
    {
        
        private readonly IBinaryObjectManager _binaryObjectManager;
        private readonly TenantManager _tenantManager;
        private readonly LocalProfileImageService _localProfileImageService;
        public DocumentAttachmentController(IBinaryObjectManager binaryObjectManager, TenantManager tenantManager, LocalProfileImageService localProfileImageService = null)
        {
            _binaryObjectManager = binaryObjectManager;
            _tenantManager = tenantManager;
            _localProfileImageService = localProfileImageService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> UploadAttachments()
        {
            try
            {
                var logoObject = await UploadLogoFileInternal();

                var tenant = await _tenantManager.GetByIdAsync(AbpSession.GetTenantId());
                ////tenant.DarkLogoId = logoObject.id;
                ////tenant.DarkLogoFileType = logoObject.contentType;

                return Json(new AjaxResponse(new
                {
                    id = logoObject.id,
                    TenantId = tenant.Id,
                    fileType = tenant.DarkLogoFileType
                }));
            }
            catch (UserFriendlyException ex)
            {
                return Json(new AjaxResponse(new ErrorInfo(ex.Message)));
            }
        }

        private async Task<(Guid id, string contentType)> UploadLogoFileInternal()
        {
            var logoFile = Request.Form.Files.First();

            //Check input
            if (logoFile == null)
            {
                throw new UserFriendlyException(L("File_Empty_Error"));
            }

            if (logoFile.Length > 5242880) //100KB
            {
                throw new UserFriendlyException(L("File_SizeLimit_Error"));
            }

            byte[] fileBytes;
            await using (var stream = logoFile.OpenReadStream())
            {
                fileBytes = stream.GetAllBytes();
               // _imageValidator.ValidateDimensions(fileBytes, 512, 128);
            }

            var logoObject = new BinaryObject(AbpSession.GetTenantId(), fileBytes, $"Logo {DateTime.UtcNow}");
            await _binaryObjectManager.SaveAsync(logoObject);
            return (logoObject.Id, logoFile.ContentType);
        }
        public async Task<IActionResult> DownloadFile(Guid fileId, string fileName)
        {
            // Retrieve the file from the database based on the identifier
            var fileData = await _binaryObjectManager.GetOrNullAsync(fileId);
             
            if (fileData == null)
            {
                return NotFound(); // File not found
            }

            var fileType = GetContentType(fileName);

            // Return the file as a response
            return File(fileData.Bytes, fileType, fileName); 
        }

        private string GetContentType(string fileName)
        {
            var ext = Path.GetExtension(fileName)?.ToLowerInvariant();

            switch (ext)
            {
                case ".jpg":
                case ".jpeg":
                    return "image/jpeg";
                   
                case ".png":
                    return "image/png";
                  
                case ".gif":
                    return "image/gif";
                    
                case ".pdf":
                    return "application/pdf";
                   
                case ".txt":
                case ".text":
                case ".plain":
                    return "text/plain";
                    
                case ".doc":
                case ".docx":
                    return "application/msword";
                  
                // Add more cases as needed for different file types

                // Default content type for unknown file types
                default:
                    return "application/octet-stream";
                  
            }
        }
    }
}
