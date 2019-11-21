import { InverterDataGranular } from "../Definitions/InverterDataGranular";
import { inject, injectable } from "inversify";
import { InverterDataService } from "../Services/inverter-data-service";



@injectable()
class DashChartDriver {

    public test: string;

    private data: InverterDataService;

    constructor(@inject(InverterDataService) dataService: InverterDataService) {
        this.data = dataService;
    }


    //public SetData(inverterData: InverterDataGranular) {


    //}

}
