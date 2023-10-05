using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMSetup.Dtos
{
    public class GetSubjectForEditOutput
    {
        public CreateOrEditSubjectDto Subject { get; set; }

        public string SubjectAreaName { get; set; }

    }
}