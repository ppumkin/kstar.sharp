using System.Collections.Generic;
using System.Linq;
using kstar.sharp.Models;
using kstar.sharp.Type;

namespace kstar.sharp.aspnetcore.Models
{
    public class HoursStatViewModel
    {
        public List<HourlyStatistic> HourlyStats { get; set; }
        public KiloWattHour TotalProduction => HourlyStats.Sum(e => e.Production);
        public KiloWattHour TotalConsumption => HourlyStats.Sum(e => e.Consumption);
    }
}
