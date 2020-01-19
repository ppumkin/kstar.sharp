"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
require("reflect-metadata");
var inversify_1 = require("inversify");
var inverter_data_service_1 = require("./Services/inverter-data-service");
var dash_driver_1 = require("./Driver/dash.driver");
var container = new inversify_1.Container();
container.bind(inverter_data_service_1.InverterDataService).to(inverter_data_service_1.InverterDataService); //.inSingletonScope();
container.bind(dash_driver_1.DashDriver).to(dash_driver_1.DashDriver); //.inSingletonScope();
//container.bind<Bot>(TYPES.Bot).to(Bot).inSingletonScope();
//container.bind<Client>(TYPES.Client).toConstantValue(new Client());
//container.bind<string>(TYPES.Token).toConstantValue(process.env.TOKEN);
exports.default = container;
//# sourceMappingURL=inversify.config.js.map