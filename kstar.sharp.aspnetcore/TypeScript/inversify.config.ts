import "reflect-metadata";
import { Container } from "inversify";
import { InverterDataService } from "./Services/inverter-data-service"
//import { TYPES } from "./types";
//import { Bot } from "./bot";
//import { Client } from "discord.js";

let container = new Container();

container.bind<InverterDataService>(InverterDataService).to(InverterDataService);//.inSingletonScope();
//container.bind<Bot>(TYPES.Bot).to(Bot).inSingletonScope();
//container.bind<Client>(TYPES.Client).toConstantValue(new Client());
//container.bind<string>(TYPES.Token).toConstantValue(process.env.TOKEN);

export default container;