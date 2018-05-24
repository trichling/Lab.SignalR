import Vue from "vue";
import { Component, Lifecycle } from "av-ts";
import * as signalR from "@aspnet/signalr";

declare var room : string;

@Component({
    name: "chat-client",
    template: require("./ChatClient.html")
})
export default class ChatClient extends Vue {

    private connection = new signalR.HubConnectionBuilder().withUrl(`/hubs/chat?room=${room}`).build();

    message : string = "Hallo Welt!";
    room : string = "Test";

    @Lifecycle
    created() : void {
        this.connection.on("Send", (room, msg) => alert(msg));
        this.connection.start().catch(err => alert(err));
    }

    sendMessage() : void {
        this.connection.invoke('Send', this.room, this.message).catch(err => alert('ohoh'));
    }
}