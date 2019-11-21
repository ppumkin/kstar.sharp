var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Subject } from 'rxjs';
import * as rm from 'typed-rest-client/RestClient';
import { injectable } from 'inversify';
var InverterDataService = /** @class */ (function () {
    function InverterDataService() {
        this.LatestDataObservable = new Subject();
        //var g = Observable.fromEvent()
    }
    //https://x-team.com/blog/rxjs-observables/
    //https://www.learnrxjs.io/operators/creation/fromevent.html
    InverterDataService.prototype.Update = function () {
        var _this = this;
        var apiURL = "/api/data";
        var rest = new rm.RestClient('solargui');
        var restPromise = rest.get('/api/data');
        restPromise.then(function (response) {
            if (response.statusCode == 200)
                _this.LatestDataObservable.next(response.result);
            else
                console.log("error" + response.result);
        });
        //var g = new BehaviorSubject(0);
        //g.next
        //this.http.get(apiURL)
        //const req = request(apiURL);
        //req.
        //return this.http.get(apiURL)
    };
    InverterDataService = __decorate([
        injectable(),
        __metadata("design:paramtypes", [])
    ], InverterDataService);
    return InverterDataService;
}());
export { InverterDataService };
//# sourceMappingURL=inverter-data-service.js.map