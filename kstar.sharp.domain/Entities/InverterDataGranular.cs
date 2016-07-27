using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kstar.sharp.domain.Entities
{
    public class InverterDataGranular
    {
        public DateTime RecordedDateTime { get; set; } //when Adding to table SQLite timestamp will be used when getting this will be populated

        /// <summary>
        /// Degrees Celcius of the Inverter
        /// </summary>
        public int TempCelcius { get; set; }
        /// <summary>
        /// kiloWatts. Total Energy generated during Inverter Lifetime
        /// </summary>
        public decimal ETotal { get; set; }
        /// <summary>
        /// kiloWatts. Total Energy generated betwen Midnight and Midnight. 24 Hour cycles
        /// </summary>
        public decimal EToday { get; set; }

        /// <summary>
        /// Watts. Always positive of electricity generated.
        /// </summary>
        public decimal PVPower { get; set; }
        /// <summary>
        /// Diagnostic. Volts reported on PV1 Array
        /// </summary>
        public int PV1Volt { get; set; }
        /// <summary>
        /// Diagnostic. Volts reported on PV2 Array
        /// </summary>
        public int PV2Volt { get; set; }

        /// <summary>
        /// Always positive or 0
        /// </summary>
        public decimal LoadPower { get; set; }

        /// <summary>
        /// The percentage of battery charge 0-100. Although should never reach 0!
        /// </summary>
        public int Bat1Charge { get; set; }
        /// <summary>
        /// Watts. Positive is charge. Negative is discharge.
        /// </summary>
        public decimal Bat1Power { get; set; }
        /// <summary>
        /// Diagnostic. The Voltage of the battery array
        /// </summary>
        public decimal Bat1Voltage { get; set; }
        /// <summary>
        /// Diagnostic. The Amperage of charge/discharge
        /// </summary>
        public decimal Bat1Amp { get; set; }

        /// <summary>
        /// Watts. Positive is export. Negative is import.
        /// </summary>
        public decimal GridPower { get; set; }
    }
}
