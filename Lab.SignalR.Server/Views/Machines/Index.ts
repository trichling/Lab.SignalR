import "bootstrap/dist/css/bootstrap.css";
import Vue from "vue";
import MachinesList from "./MachinesList";

new Vue({
    el: "#app",
    components: {
        "machines-list": MachinesList
    }
})