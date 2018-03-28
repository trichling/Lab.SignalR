import Vue from "vue";
import { Component, Lifecycle, Watch } from "av-ts";
import * as signalR from "@aspnet/signalr";
import Machine from "../Machines/Machine";
import MachineStatus from "./MachineStatus";

declare var MachinesAPIBaseUrl : string;


@Component({
    name: "machine-status-list",
    template: require("./MachineStatusList.html")
})
export default class MachineStatusList extends Vue {

    private machineHub? : signalR.HubConnection = undefined;
    
    groups : string[] = [];
    selectedGroup : string = "";
    machinesStatus : MachineStatus[] = [];

    @Lifecycle
    created() : void {
        fetch(`${MachinesAPIBaseUrl}/groups`)
            .then(response => response.json() as Promise<string[]>)
            .then(data => {
                this.groups = data;
            });
    }

    @Watch("selectedGroup")
    selectedGroupChanged(newValue : string, oldValue : string)
    {
        if (this.machineHub === undefined) {
            this.connectToMachineHubInGroup();
            return; 
        } 
        
        this.machineHub.stop().then(this.connectToMachineHubInGroup);
        this.machinesStatus = [];
    }

    sendMessage(machineStatus : MachineStatus) {
        if (this.machineHub === undefined)
            return;

        this.machineHub.invoke("NotifyMachine", machineStatus.machine.group, machineStatus.machine.name, machineStatus.message)
    }

    private connectToMachineHubInGroup() : void {
        this.machineHub = new signalR.HubConnection(`/hubs/machines?group=${this.selectedGroup}`, { transport: signalR.TransportType.WebSockets });
        this.machineHub.on("ReportMachineSpeed", this.machineSpeedReported);
        this.machineHub.start().catch(err => alert(err));
    }

    private machineSpeedReported(group : string, name : string, speed : number) {
        var machineStatus = this.machinesStatus.find(m => m.name == name);

        if (machineStatus === undefined)
        {
            machineStatus = new MachineStatus(new Machine(group, name));
            this.machinesStatus.push(machineStatus);
        }

        machineStatus.pushSpeed(speed);
    }
}