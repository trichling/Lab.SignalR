import Machine from "./Machine";
import { createWorker, ITypedWorker } from 'typed-web-workers'
import * as signalR from "@aspnet/signalr";

export default class PublishMachineStatusWorker {

    worker : ITypedWorker<Machine, void> = createWorker(this.postStatus, this.handleResult);
    
    constructor(public machineHub : signalR.HubConnection) {

    }

    postStatus(machine : Machine) : void {
        this.machineHub.invoke("ReportMachineSpeed", machine.group, machine.name, machine.speedMeterPerSecond)
    }
    
    handleResult() : void {
        console.log("Status posted");
    }
    
    

}

