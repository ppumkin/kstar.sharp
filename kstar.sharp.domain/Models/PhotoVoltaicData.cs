using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using kstar.sharp.domain.Extensions;

namespace kstar.sharp.domain.Models
{
    public class PhotoVoltaicData : InverterDataBase
    {

        public override string ToString()
        {
            string toString = String.Format(@"
PV:{0}W ({1}v/{2}v) ",
                PVPower, PV1Volt, PV2Volt
            );

            return toString;
        }

        public decimal PVPower { get { return (Decimal.Add(Decimal.Multiply(vpv1, ipv1), Decimal.Multiply(vpv2, ipv2))); } }
        public Int32 PV1Volt { get { return Convert.ToInt32(Decimal.Round(vpv1, 0)); } }
        public Int32 PV2Volt { get { return Convert.ToInt32(Decimal.Round(vpv2, 0)); } }





        private decimal vpv1 = 0;
        private decimal ipv1 = 0;
        private decimal vpv2 = 0;
        private decimal ipv2 = 0;
        private decimal pv1Mode = 0;
        private decimal pv2Mode = 0;

        public PhotoVoltaicData(string[] HEXData)
            : base(HEXData)
        {

            try
            {
                vpv1 = Decimal.Divide((HEXData[7] + HEXData[8]).HexToDecimal(), 10);
                ipv1 = Decimal.Divide((HEXData[9] + HEXData[10]).HexToDecimal(), 10);
                vpv2 = Decimal.Divide((HEXData[12] + HEXData[13]).HexToDecimal(), 10);
                ipv2 = Decimal.Divide((HEXData[14] + HEXData[15]).HexToDecimal(), 10);
                pv1Mode = HEXData[14].HexToDecimal();
                pv2Mode = HEXData[16].HexToDecimal();
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
