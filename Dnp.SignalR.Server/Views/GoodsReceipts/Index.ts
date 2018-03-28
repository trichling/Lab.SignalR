import "bootstrap/dist/css/bootstrap.css";
import Vue from "vue";
import VueRouter from "vue-router";
import GoodsReceiptList from "./GoodsReceitpsList";
import GoodsReceiptDetails from "./GoodsReceiptDetails";

Vue.use(VueRouter);

const routes = [
    { path: '/all', component: GoodsReceiptList },
    { path: '/details/:goodsReceiptId', component: GoodsReceiptDetails },
    { path: '*', redirect: '/all' }
];

new Vue({
    el: "#app",
    router: new VueRouter({ base: '/goodsreceipt/index/', mode: 'history', routes: routes  })
})