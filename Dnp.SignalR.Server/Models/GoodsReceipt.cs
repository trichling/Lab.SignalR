using System.Collections.Generic;

namespace Dnp.SignalR.Server.Models
{

    public class GoodsReceipt
    {

        public GoodsReceipt()
        {
            Positions = new List<GoodsReceiptPosition>();
            Comments = new List<string>();
        }

        public int Id { get; set; }

        public List<GoodsReceiptPosition> Positions { get; set; }

        public List<string> Comments { get; set; }

    }

    public class GoodsReceiptPosition
    {

        public string ArticleNumber { get; set; }

        public string ArticleDescription { get; set; }

        public int OrderedAmount { get; set; }

        public int DeliveredAmount { get; set; }


    }
}