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
    groupMessage: string = "";

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

    sendMachineMessage(machineStatus : MachineStatus) {
        if (this.machineHub === undefined)
            return;

        this.machineHub.invoke("NotifyMachine", machineStatus.machine.group, machineStatus.machine.name, machineStatus.message);
        machineStatus.message = "";
    }

    sendGroupMessage() {
        if (this.machineHub === undefined)
            return;

        this.machineHub.invoke("NotifyGroup", this.selectedGroup, this.groupMessage);
        this.groupMessage = "";
    }

    sendAllMessage() {
        if (this.machineHub === undefined)
            return;

        this.machineHub.invoke("NotifyAll", this.groupMessage);
        this.groupMessage = "";
    }

    private connectToMachineHubInGroup() : void {
        this.machineHub = new signalR.HubConnectionBuilder().withUrl(`/hubs/machines?group=${this.selectedGroup}`).build();
        this.machineHub.on("MachineSpeedReported", this.machineSpeedReported);
        this.machineHub.on("NotifyGroup", (group, message) => { (this as any).$toast(`An ${group}: ${message}`) } );
        this.machineHub.on("NotifyAll", (message) => { (this as any).$toast(`An alle: ${message}`) } );
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