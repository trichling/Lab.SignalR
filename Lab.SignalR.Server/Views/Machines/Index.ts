import "bootstrap/dist/css/bootstrap.css";
import "vue-easy-toast/types/index";
import Vue from "vue";
import Toast  from "vue-easy-toast";
import MachinesList from "./MachinesList";

Vue.use(Toast);

new Vue({
    el: "#app",
    components: {
        "machines-list": MachinesList
    }
})