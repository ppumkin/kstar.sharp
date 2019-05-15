namespace kstar.sharp.domain.Extensions
{
    public static class InverterDataExtensions
    {
        /// <summary>
        /// Converts Model to Entity
        /// </summary>
        /// <param name="inverterModelData"></param>
        /// <returns></returns>
        public static kstar.sharp.domain.Entities.InverterDataGranular ToEntity(this kstar.sharp.domain.Models.InverterData inverterModelData)
        {
            kstar.sharp.domain.Entities.InverterDataGranular entity = new kstar.sharp.domain.Entities.InverterDataGranular()
            {
                EToday = inverterModelData.StatData.EnergyToday,
                ETotal = inverterModelData.StatData.EnergyTotal,
                TempCelcius = inverterModelData.StatData.InverterTemperature,

                GridPower = inverterModelData.GridData.GridPower,
                LoadPower = inverterModelData.LoadData.LoadPower,

                Bat1Amp = inverterModelData.BatteryData.Battery1Amp,
                Bat1Charge = inverterModelData.BatteryData.Battery1Charge,
                Bat1Power = inverterModelData.BatteryData.Battery1Power,
                Bat1Voltage = inverterModelData.BatteryData.Battery1Volt,

                PV1Volt = inverterModelData.PVData.PV1Volt,
                PV2Volt = inverterModelData.PVData.PV2Volt,
                PVPower = inverterModelData.PVData.PVPower,

                RecordedDateTime = inverterModelData.RecordDateTime
            };

            return entity;
        }

    }
}
