"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
require("reflect-metadata");
var inversify_1 = require("inversify");
var inverter_data_service_1 = require("./Services/inverter-data-service");
//import { TYPES } from "./types";
//import { Bot } from "./bot";
//import { Client } from "discord.js";
var container = new inversify_1.Container();
container.bind(inverter_data_service_1.InverterDataService).to(inverter_data_service_1.InverterDataService); //.inSingletonScope();
//container.bind<Bot>(TYPES.Bot).to(Bot).inSingletonScope();
//container.bind<Client>(TYPES.Client).toConstantValue(new Client());
//container.bind<string>(TYPES.Token).toConstantValue(process.env.TOKEN);
exports.default = container;
//# sourceMappingURL=inversify.config.js.map