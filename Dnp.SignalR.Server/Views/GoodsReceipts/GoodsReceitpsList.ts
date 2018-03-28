import Vue from "vue";
import { Component, Lifecycle } from "av-ts";
import { GoodsReceipt } from "./GoodsReceipt";

declare var GoodsReceiptsAPIBaseUrl : string;

@Component({
    name: "goods-receipt-list",
    template: require("./GoodsReceiptsList.html")
})
export default class GoodsReceiptList extends Vue {

    goodsReceipts : GoodsReceipt[] = [];

    @Lifecycle
    mounted() {
        fetch(`${GoodsReceiptsAPIBaseUrl}`)
            .then(response => response.json() as Promise<GoodsReceipt[]>)
            .then(data => {
                this.goodsReceipts = data;
            });
    }
}