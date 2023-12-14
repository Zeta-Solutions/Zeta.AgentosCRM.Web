using Abp.Extensions;
using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Zeta.AgentosCRM.CRMPartner.Profile.Dtos
{
    public class UpdatePartnerProfilePictureInput : ICustomValidate
    {
        [MaxLength(400)]
        public string FileToken { get; set; }

        public bool UseGravatarProfilePicture { get; set; }

        public long? PartnerId { get; set; }

        public void AddValidationErrors(CustomValidationContext context)
        {
            if (!UseGravatarProfilePicture && FileToken.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(FileToken));
            }
        }
    }
}
