using Abp.Extensions;
using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Zeta.AgentosCRM.CRMProducts.Profile.Dtos
{
    public class UpdateProductProfilePictureInput : ICustomValidate
    {
        [MaxLength(400)]
        public string FileToken { get; set; }

        public bool UseGravatarProfilePicture { get; set; }

        public long? ProductId { get; set; }

        public void AddValidationErrors(CustomValidationContext context)
        {
            if (!UseGravatarProfilePicture && FileToken.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(FileToken));
            }
        }
    }
}
