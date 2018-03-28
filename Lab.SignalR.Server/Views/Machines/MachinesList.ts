import Vue from "vue";
import { Component, Lifecycle, Watch } from "av-ts";
import * as signalR from "@aspnet/signalr";
import vueSlider from "vue-slider-component";
import Machine from "./Machine";

declare var MachinesAPIBaseUrl : string;

@Component({
    name: "machines-list",
    template: require("./MachinesList.html"),
    components: {
        "vue-slider": vueSlider
    }
})
export default class MachinesList extends Vue {

    private machineHub? : signalR.HubConnection = undefined;

    groups : string[] = [];
    selectedGroup : string = "";
    machines : { [id : string] : Machine[] } = {};
    machinesInSelectedGroup : Machine[] = [];

    constructor() {
        super();
        this.machineHub = new signalR.HubConnection(`/hubs/machines`, { transport: signalR.TransportType.WebSockets });
        this.machineHub.on("ReportMachineSpeed", (g, n, s) => { alert(n); } )
        this.machineHub.start().catch(err => alert(err));
        this.reportMachineSpeed();
    } 

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
        fetch(`${MachinesAPIBaseUrl}/machines/${newValue}`)
            .then(response => response.json() as Promise<Machine[]>)
            .then(data => {
                if (!this.machines[newValue])
                    this.machines[newValue] = data;

                this.machinesInSelectedGroup = this.machines[newValue];
            });
    }

    private reportMachineSpeed() : void {
        setInterval(
            () => {
                if (this.machineHub === undefined)
                    return;

                for (let group in this.machines) {
                    for (let machine of this.machines[group])
                        this.machineHub.invoke("ReportMachineSpeed", group, machine.name, machine.speedMeterPerSecond)
            }
        }, 1000);
    }
}