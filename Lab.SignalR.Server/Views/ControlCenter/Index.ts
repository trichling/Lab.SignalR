import "bootstrap/dist/css/bootstrap.css";
import "./Gauge";
import "./SpeedHistogram";
import Vue from "vue";
import Toast  from "vue-easy-toast";
import MachineStatusList from "./MachineStatusList";

Vue.use(Toast);

new Vue({
    el: "#app",
    components: {
        "machine-status-list" : MachineStatusList
    }
})