using System;
using System.Collections.Generic;
using System.Text;
using kstar.sharp.Type;

namespace kstar.sharp.Models
{
    public class HourlyStatistic
    {
        public DateTime Hour { get; set; }
        public KiloWattHour Production { get; set; }
        public KiloWattHour Consumption { get; set; }
    }
}
