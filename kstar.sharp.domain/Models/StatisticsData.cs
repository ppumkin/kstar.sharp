using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using kstar.sharp.domain.Extensions;


namespace kstar.sharp.domain.Models
{
    public class StatisticsData : InverterDataBase
    {


        public override string ToString()
        {
            string toString = String.Format(@"
ETotal:{0}kW   EToday:{1}kW   HTotal:{2}hours/{3}days   Temp:{4}°C",
                EnergyTotal, EnergyToday, LifetimeHours, LifetimeDays, InverterTemperature
            );

            return toString;
        }

        public Int64 LifetimeHours { get { return Convert.ToInt64(Htotal) ; } }
        public Int64 LifetimeDays { get { return Convert.ToInt64( Decimal.Round(LifetimeHours / 24, 0) ); } }
        public Int32 InverterTemperature { get { return Convert.ToInt32(Decimal.Round(temp,0)); } }
        public decimal EnergyToday { get { return Eday; } }
        public decimal EnergyTotal { get { return Etotal; } }



        private decimal temp = 0;
        private decimal Etotal = 0;
        private decimal Htotal = 0;
        private decimal Eday = 0;

        public StatisticsData(string[] HEXData)
            : base(HEXData)
        {

            try
            {
                temp = Decimal.Divide((HEXData[60] + HEXData[61]).HexToDecimal(), 10);
                Etotal = Decimal.Divide((HEXData[66] + HEXData[67] + HEXData[68] + HEXData[69]).HexToDecimal(), 10);
                Eday = Decimal.Divide((HEXData[74] + HEXData[75]).HexToDecimal(), 10);
                Htotal = (HEXData[70] + HEXData[71] + HEXData[72] + HEXData[73]).HexToDecimal();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
