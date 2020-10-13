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

        [HttpGet]
        public async Task<IActionResult> GetOrders([FromQuery(Name = "TableKeys")] IEnumerable<string> TableKeys, [FromQuery(Name = "Owners")] IEnumerable<string> Owners)
        {
            var output = await _orderManager.GetOrders(TableKeys, Owners);
            return Ok(output);
        }
        [HttpGet("{TableKey}")]
        public async Task<IActionResult> GetOrdersItemsForTable([FromRoute] string TableKey)
        {
            var output = await _orderManager.GetTableOrderItems(TableKey);
            return Ok(output);
        }

    }
}
