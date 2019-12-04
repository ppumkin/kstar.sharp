"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var inversify_config_1 = require("../inversify.config");
var dash_driver_1 = require("../Driver/dash.driver");
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
var driver = inversify_config_1.default.get(dash_driver_1.DashDriver);
debugger;
driver.Lala();
//# sourceMappingURL=dash.view.js.map