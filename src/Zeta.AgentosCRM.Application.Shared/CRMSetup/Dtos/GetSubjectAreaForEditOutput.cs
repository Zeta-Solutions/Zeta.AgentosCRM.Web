using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMSetup.Dtos
{
    public class GetSubjectAreaForEditOutput
    {
        public CreateOrEditSubjectAreaDto SubjectArea { get; set; }

    }
}