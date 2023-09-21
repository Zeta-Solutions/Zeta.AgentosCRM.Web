using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zeta.AgentosCRM.MultiTenancy.HostDashboard.Dto;

namespace Zeta.AgentosCRM.MultiTenancy.HostDashboard
{
    public interface IIncomeStatisticsService
    {
        Task<List<IncomeStastistic>> GetIncomeStatisticsData(DateTime startDate, DateTime endDate,
            ChartDateInterval dateInterval);
    }
}