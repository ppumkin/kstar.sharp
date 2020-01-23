using System;
using System.Collections.Generic;
using System.Text;

namespace kstar.sharp.Type
{
    //[JsonObject(MemberSerialization.OptIn)]
    //[TypeConverter(typeof(DecimalConverter))]
    public struct KiloWattHour
    {
        //[JsonProperty(IsReference = true, PropertyName = "")]
        public decimal Value => _value;
        //public decimal _value;
        //public decimal Value => _value;
        private readonly decimal _value;

        public KiloWattHour(decimal value)
        {
            _value = value;
        }

        public static implicit operator decimal(KiloWattHour d) => d._value;
        public static implicit operator KiloWattHour(decimal kwh) => new KiloWattHour(kwh);


        //public static explicit operator KiloWattHour(decimal d) => new KiloWattHour(d); 
        //public static explicit operator decimal(KiloWattHour kwh)  => kwh._value;

        //public static implicit operator JToken(KiloWattHour d) => d._value;

        public override string ToString() => $"{_value:n2}kWh";
    }
}
