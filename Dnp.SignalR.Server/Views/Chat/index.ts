import "bootstrap/dist/css/bootstrap.css";
import Vue from "vue";
import ChatClient from "./ChatClient";

new Vue({
    el: "#app",
    components: {
        "chat-client" : ChatClient
    }
})