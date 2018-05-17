import * as signalR from "@aspnet/signalr";

export default class Machine {

    private machineHub? : signalR.HubConnection = undefined;


    constructor(group: string, name : string) {
            this.group = group,
            this.name = name;
            this.machineHub = new signalR.HubConnection(`/hubs/machines?group=${this.group}&name=${this.name}`, { transport: signalR.TransportType.WebSockets });
            this.machineHub.on("NotifyMachine", (group, name, message) => { this.messages += `${message}\r\n` } );
            this.machineHub.on("NotifyGroup", (group, message) => { this.messages += `An ${group}: ${message}` } );
            this.machineHub.on("NotifyAll", (message) => { this.messages += `ACHTUNG: An alle: ${message}\r\n` } );
            this.machineHub.start().catch(err => alert(err));
            this.reportMachineSpeed();
    }

    group: string = "";
    name: string = "";
    speedMeterPerSecond : number = 0;
    messages: string = "";

    private reportMachineSpeed() : void {
        setInterval(
            () => {
                if (this.machineHub === undefined)
                    return;
                this.machineHub.invoke("ReportMachineSpeed", this.group, this.name, this.speedMeterPerSecond)
        }, 1000);
    }
}