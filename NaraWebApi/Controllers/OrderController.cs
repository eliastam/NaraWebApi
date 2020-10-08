using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NaraWebApi.Data.Entities;
using NaraWebApi.DTO;
using NaraWebApi.Services;

namespace NaraWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {

        private readonly IOrderManager _orderManager;

        public OrderController(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }


        [HttpPost]

        public async Task<IActionResult> CreateOrder([FromBody] ContentOrder contentOrder)
        {
            var order = await _orderManager.MakeAnOrder(contentOrder);
            return Ok(order);
        }
        /**
         * 
         * 
         * TableController ==> get all terrasses + all resto Tables WitjhOrders
         * Get AllItems ; 
         */
    }
}
