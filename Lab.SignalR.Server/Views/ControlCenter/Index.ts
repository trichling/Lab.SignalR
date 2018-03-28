import "bootstrap/dist/css/bootstrap.css";
import "./Gauge";
import "./SpeedHistogram";
import Vue from "vue";
import MachineStatusList from "./MachineStatusList";

new Vue({
    el: "#app",
    components: {
        "machine-status-list" : MachineStatusList
    }
})