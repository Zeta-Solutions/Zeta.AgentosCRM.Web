using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Zeta.AgentosCRM.CRMAppointments.Invitees.Dtos;

namespace Zeta.AgentosCRM.CRMProducts.Dtos
{
    public class GetProductForEditOutput
    {
        public CreateOrEditProductDto Product { get; set; }

        public string PartnerPartnerName { get; set; }

        public string PartnerTypeName { get; set; }

        public string BranchName { get; set; }

        public List<CreateOrEditProductBranchDto> Branches { get; set; }

        public string BinaryObjectDescription { get; set; }
    }
}