using System;
using System.Collections.Generic;
using System.Linq;
using kstar.sharp.domain.Entities;
using kstar.sharp.Models;
using kstar.sharp.Type;

namespace kstar.sharp.aspnetcore.Models
{
    public class HoursStatViewModel
    {
        public InverterDataGranular Latest { get; set; }
        public List<HourlyStatistic> HourlyStats { get; set; }
        public KiloWattHour TotalProduction => HourlyStats.Sum(e => e.Production);
        public KiloWattHour TotalConsumption => HourlyStats.Sum(e => e.Consumption);
        public KiloWattHour TotalPurchased => decimal.Subtract(TotalConsumption, TotalProduction);
    }
}
