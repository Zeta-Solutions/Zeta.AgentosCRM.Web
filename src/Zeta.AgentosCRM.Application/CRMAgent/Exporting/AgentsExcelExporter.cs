using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone; 
using Zeta.AgentosCRM.CRMAgent.Dtos;
using Zeta.AgentosCRM.Dto;
using Zeta.AgentosCRM.Storage;
using Zeta.AgentosCRM.DataExporting.Excel.MiniExcel;
using Zeta.AgentosCRM.CRMAgent;
using Stripe;

namespace Zeta.AgentosCRM.CRMAgent.Exporting
{
    public class AgentsExcelExporter : MiniExcelExcelExporterBase, IAgentsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public AgentsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetAgentForViewDto> agents)
        {
            var excelPackage =new List<Dictionary<string, object>>();

            foreach (var agent in agents)
            {
                excelPackage.Add(new Dictionary<string, object>
                {
                    {   L("Name"), agent.Agent.Name },
                    {   L("IsSuperAgent"), agent.Agent.IsSuperAgent },
                    {   L("IsBusiness"), agent.Agent.IsBusiness },
                    {   L("PhoneNo"), agent.Agent.PhoneNo },
                    {   L("PhoneCode"), agent.Agent.PhoneCode },
                    {   L("Email"), agent.Agent.Email },
                    {   L("City"), agent.Agent.City },
                    {   L("Street"), agent.Agent.Street },
                    {   L("State"), agent.Agent.State },
                    {   L("IncomeSharingPer"), agent.Agent.IncomeSharingPer },
                    {   L("Tax"), agent.Agent.Tax }, 
                    {   L("PrimaryContactName"), agent.Agent.PrimaryContactName },
                    {   L("TaxNo"), agent.Agent.TaxNo },
                    {   L("ContractExpiryDate"), agent.Agent.ContractExpiryDate },
                    {   L("ClaimRevenuePer"), agent.Agent.ClaimRevenuePer },
                });
            }

            return CreateExcelPackage( "Agents.xlsx", excelPackage);
        }
    }
}