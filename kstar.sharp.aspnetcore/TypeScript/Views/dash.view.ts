import container from "../inversify.config";
import { DashDriver } from '../Driver/dash.driver';


//@injectable()
//class DashView {

//    public LatestDataObservable: Subject<InverterDataGranular>;

//    private data: InverterDataService;

//    constructor(@inject(InverterDataService) dataService: InverterDataService) {
//        this.data = dataService;
//    }

//    public Lala() {

//        this.data.LatestDataObservable.subscribe((data) => {
//            console.log(data);
//        });
//    }
//}



let driver = container.get<DashDriver>(DashDriver);

debugger;
driver.Lala();