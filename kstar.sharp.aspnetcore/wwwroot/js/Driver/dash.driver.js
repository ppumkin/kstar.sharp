"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __param = (this && this.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};
Object.defineProperty(exports, "__esModule", { value: true });
var inversify_1 = require("inversify");
var inverter_data_service_1 = require("../Services/inverter-data-service");
var DashDriver = /** @class */ (function () {
    function DashDriver(dataService) {
        this.dataService = dataService;
    }
    DashDriver.prototype.Lala = function () {
        var _this = this;
        this.dataService.LatestDataObservable.subscribe(function (data) {
            _this.inverterDataGranular = data;
            console.log(_this.inverterDataGranular);
        });
    };
    DashDriver = __decorate([
        inversify_1.injectable(),
        __param(0, inversify_1.inject(inverter_data_service_1.InverterDataService)),
        __metadata("design:paramtypes", [inverter_data_service_1.InverterDataService])
    ], DashDriver);
    return DashDriver;
}());
exports.DashDriver = DashDriver;
//# sourceMappingURL=dash.driver.js.map