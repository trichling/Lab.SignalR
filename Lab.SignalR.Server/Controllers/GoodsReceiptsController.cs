using System;
using System.Collections.Generic;
using System.Linq;
using Lab.SignalR.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab.SignalR.Server.Controllers 
{

    [Route("goodsreceipts")]
    public class GoodsReceiptsController : Controller
    {
        
        private static Dictionary<int, GoodsReceipt> _repository = new Dictionary<int, GoodsReceipt>() {
            {1, new GoodsReceipt() 
                { Id = 1, 
                  Positions = new List<GoodsReceiptPosition>() 
                    { 
                        new GoodsReceiptPosition() { ArticleNumber = "1", ArticleDescription = "Apfel", OrderedAmount = 5, DeliveredAmount = 5 },
                        new GoodsReceiptPosition() { ArticleNumber = "2", ArticleDescription = "Birne", OrderedAmount = 7, DeliveredAmount = 7 },
                        new GoodsReceiptPosition() { ArticleNumber = "3", ArticleDescription = "Banane", OrderedAmount = 3, DeliveredAmount = 3 },
                    },
                  Comments = new List<string>() 
                    {
                        "Hallo",
                        "Welt"
                    }
                }
            },
            {2, new GoodsReceipt() 
                { Id = 2, 
                  Positions = new List<GoodsReceiptPosition>() 
                    { 
                        new GoodsReceiptPosition() { ArticleNumber = "1", ArticleDescription = "Apfel", OrderedAmount = 10, DeliveredAmount = 10 },
                        new GoodsReceiptPosition() { ArticleNumber = "2", ArticleDescription = "Birne", OrderedAmount = 17, DeliveredAmount = 17 },
                        new GoodsReceiptPosition() { ArticleNumber = "4", ArticleDescription = "Pfirsich", OrderedAmount = 8, DeliveredAmount = 8 },
                    },
                  Comments = new List<string>() 
                    {
                        "Tolles",
                        "Ding"
                    } 

                }
            }
        };

        public GoodsReceiptsController()
        {
        }

        

        [Route("index")]
        [HttpGet]
        public IActionResult Index()
        {
            //ViewData["room"] = room;
            return View();
        }

        [Route("")]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_repository.Values.ToList());
        }

        [Route("{id}")]
        [HttpGet]
        public IActionResult GetById([FromRoute] int id)
        {
            return Ok(_repository[id]);
        }

        [Route("")]
        [HttpPost]
        public IActionResult Create([FromBody] GoodsReceipt goodsReceipt)
        {
            lock (this)
            {
                var nextId = _repository.Count + 1;
                goodsReceipt.Id = nextId;
                _repository.Add(goodsReceipt.Id, goodsReceipt);
            }
            return Ok();
        }

        [Route("{id}/comments")]
        [HttpPost]
        public IActionResult Create([FromRoute] int id, [FromBody] string comment)
        {
            lock (this)
            {
                _repository[id].Comments.Add(comment);
            }
            return Ok();
        }

      
    }

}