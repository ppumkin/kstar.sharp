import "reflect-metadata";
import { Container } from "inversify";
import { InverterDataService } from "./Services/inverter-data-service";
import { DashDriver } from "./Driver/dash.driver";
var container = new Container();
container.bind(InverterDataService).to(InverterDataService); //.inSingletonScope();
container.bind(DashDriver).to(DashDriver); //.inSingletonScope();
//container.bind<Bot>(TYPES.Bot).to(Bot).inSingletonScope();
//container.bind<Client>(TYPES.Client).toConstantValue(new Client());
//container.bind<string>(TYPES.Token).toConstantValue(process.env.TOKEN);
export default container;
//# sourceMappingURL=inversify.config.js.map