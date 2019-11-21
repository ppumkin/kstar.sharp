import {  Subject  } from 'rxjs';
import { InverterDataGranular } from "../Definitions/InverterDataGranular";
import *  as rm from 'typed-rest-client/RestClient';
import { injectable } from 'inversify';


@injectable()
export class InverterDataService {

    public LatestDataObservable: Subject<InverterDataGranular>;

    constructor() {
        this.LatestDataObservable = new Subject();

        //var g = Observable.fromEvent()

    }

    //https://x-team.com/blog/rxjs-observables/
    //https://www.learnrxjs.io/operators/creation/fromevent.html

    public Update(): void { //test: InverterDataGranular) {
        const apiURL = `/api/data`;

        let rest: rm.RestClient = new rm.RestClient('solargui');

        let restPromise = rest.get<InverterDataGranular>('/api/data');

        restPromise.then((response) => {
            if (response.statusCode == 200)
                this.LatestDataObservable.next(response.result);
            else
                console.log("error" + response.result);
        });


        //var g = new BehaviorSubject(0);
        //g.next

        //this.http.get(apiURL)
        //const req = request(apiURL);
        //req.
        //return this.http.get(apiURL)
    }
}

