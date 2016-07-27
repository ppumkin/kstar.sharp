using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kstar.sharp.domain.Models
{
    public class InverterDataBase
    {

        protected int INVERTER_VERSION = 1;
        protected string hexData;

        public InverterDataBase(string[] HEXArray)
        {
            if (HEXArray.Length != 92)
                throw new ArgumentException("HEX Array size incorrect");

            INVERTER_VERSION = 1; //There is some significance in the packet size but not sure exactly what yet

            hexData = String.Join("", HEXArray);

            if (!hexData.StartsWith("AA557FB0018653")) //Maybe other inverters have different starting data on the packets that are parsed differently?
                throw new ArgumentException("Inverter Signature not recognised"); 

            //All is good, Carry on McCarrying on!
        }

        public InverterDataBase()
        {


        }
    }
}
