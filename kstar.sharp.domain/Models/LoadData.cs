using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using kstar.sharp.domain.Extensions;


namespace kstar.sharp.domain.Models
{
    public class LoadData : InverterDataBase
    {


        public override string ToString()
        {
            string toString = String.Format(@"
Load:{0}W",
                LoadPower
            );

            return toString;
        }

        public decimal LoadPower { get { return Pload; } }



        decimal Vload = 0;
        decimal Iload = 0;
        decimal Pload = 0;
        decimal Fload = 0;

        public LoadData(string[] HEXData)
            : base(HEXData)
        {

            try
            {

                Vload = Decimal.Divide((HEXData[50] + HEXData[51]).HexToDecimal(), 10);
                Iload = Decimal.Divide((HEXData[52] + HEXData[53]).HexToDecimal(), 10);
                Pload = (HEXData[54] + HEXData[55]).HexToDecimal();  //Decimal.Divide((HEXData[54] + HEXData[55]).HexToDecimal(), 1000);
                Fload = Decimal.Divide((HEXData[56] + HEXData[57]).HexToDecimal(), 10);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
