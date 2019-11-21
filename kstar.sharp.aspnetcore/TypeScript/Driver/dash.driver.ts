import { InverterDataGranular } from "../Definitions/InverterDataGranular";
import { inject, injectable } from "inversify";
import { InverterDataService } from "../Services/inverter-data-service";


@injectable()
export class DashDriver {

    //public LatestDataObservable: Subject<InverterDataGranular>;

    private dataService: InverterDataService;
    private inverterDataGranular: InverterDataGranular;

    constructor(@inject(InverterDataService) dataService: InverterDataService) {
        this.dataService = dataService;
    }

    public Lala() {

        this.dataService.LatestDataObservable.subscribe((data) => {
            this.inverterDataGranular = data;

            console.log(this.inverterDataGranular);
        });
    }
}