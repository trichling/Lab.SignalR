import Vue from "vue";
import { Component, Lifecycle } from "av-ts";
import { GoodsReceipt } from "./GoodsReceipt";

declare var GoodsReceiptsAPIBaseUrl : string;

@Component({
    name: "goods-receipt-details",
    template: require("./GoodsReceiptDetails.html")
})
export default class GoodsReceiptDetails extends Vue {

    goodsReceipt : GoodsReceipt = new GoodsReceipt(0, [], []);

    @Lifecycle
    mounted() : void {
        fetch(`${GoodsReceiptsAPIBaseUrl}/${this.$route.params.goodsReceiptId}`)
            .then(response => response.json() as Promise<GoodsReceipt>)
            .then(data => {
                this.goodsReceipt = data;
            });
    }

}