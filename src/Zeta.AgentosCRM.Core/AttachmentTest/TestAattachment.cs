using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Zeta.AgentosCRM.AttachmentTest
{
    [Table("TestAattachments")]
    public class TestAattachment : Entity
    {

        [Required]
        public virtual string Description { get; set; }
        //File

        public virtual Guid? Attachment { get; set; } //File, (BinaryObjectId)

    }
}