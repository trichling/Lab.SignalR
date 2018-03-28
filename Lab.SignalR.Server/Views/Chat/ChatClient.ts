import Vue from "vue";
import { Component, Lifecycle } from "av-ts";
import * as signalR from "@aspnet/signalr";

declare var room : string;

@Component({
    name: "chat-client",
    template: require("./ChatClient.html")
})
export default class ChatClient extends Vue {

    private transportType = signalR.TransportType.WebSockets;
    private logger = new signalR.ConsoleLogger(signalR.LogLevel.Information);
    private connection = new signalR.HubConnection(`/chatHub?room=${room}`, { transport: this.transportType, logger: this.logger });

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