﻿using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMClient.Dtos
{
    public class GetClientForEditOutput
    {
        public CreateOrEditClientDto Client { get; set; }

        public string CountryDisplayProperty { get; set; }

        public string UserName { get; set; }

        public string BinaryObjectDescription { get; set; }

        public string DegreeLevelName { get; set; }

        public string SubjectAreaName { get; set; }

        public string LeadSourceName { get; set; }

        public string CountryName2 { get; set; }

        public string CountryName3 { get; set; }

    }
}