export class GoodsReceipt {

    constructor(public id : number, public positions : GoodsReceiptPosition[], comments: string[]) {}

}

export class GoodsReceiptPosition {
    constructor(public articleNumber : string, public articleDescription : string, public orderedAmount : number, public devliveredAmount : number) {}
}