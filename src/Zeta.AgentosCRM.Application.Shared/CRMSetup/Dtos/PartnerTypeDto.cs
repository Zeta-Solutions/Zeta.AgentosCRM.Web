﻿using System;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMSetup.Dtos
{
    public class PartnerTypeDto : EntityDto
    {
        public string Abbrivation { get; set; }

        public string Name { get; set; }

        public int MasterCategoryId { get; set; }

    }
}