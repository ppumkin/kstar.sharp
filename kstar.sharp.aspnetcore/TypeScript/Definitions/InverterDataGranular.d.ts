
export interface InverterDataGranular {
    TempCelcius: number;

    ETotal: number;
    EToday: number;
    PVPower: number;
    LoadPower: number;

    PV1Volt: number;
    PV2Volt: number;

    GridPower: number;

    Bat1Charge: number;
    Bat1Power: number;
    Bat1Voltage: number;
    Bat1Amp: number;
}