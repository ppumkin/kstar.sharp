using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InverterDataGranular
{

    //should be a datetime ?
    public string RecordedDateTime; //when Adding to table SQLite timestamp will be used when getting this will be populated

    /// <summary>
    /// Degrees Celcius of the Inverter
    /// </summary>
    public int TempCelcius;
    /// <summary>
    /// kiloWatts. Total Energy generated during Inverter Lifetime
    /// </summary>
    public float ETotal;
    /// <summary>
    /// kiloWatts. Total Energy generated betwen Midnight and Midnight. 24 Hour cycles
    /// </summary>
    public float EToday;

    /// <summary>
    /// Watts. Always positive of electricity generated.
    /// </summary>
    public float PVPower;
    /// <summary>
    /// Diagnostic. Volts reported on PV1 Array
    /// </summary>
    public int PV1Volt;
    /// <summary>
    /// Diagnostic. Volts reported on PV2 Array
    /// </summary>
    public int PV2Volt;

    /// <summary>
    /// Always positive or 0
    /// </summary>
    public float LoadPower;

    /// <summary>
    /// The percentage of battery charge 0-100. Although should never reach 0!
    /// </summary>
    public int Bat1Charge;
    /// <summary>
    /// Watts. Positive is charge. Negative is discharge.
    /// </summary>
    public float Bat1Power;
    /// <summary>
    /// Diagnostic. The Voltage of the battery array
    /// </summary>
    public float Bat1Voltage;
    /// <summary>
    /// Diagnostic. The Amperage of charge/discharge
    /// </summary>
    public float Bat1Amp;

    /// <summary>
    /// Watts. Positive is export. Negative is import.
    /// </summary>
    public float GridPower;
}
