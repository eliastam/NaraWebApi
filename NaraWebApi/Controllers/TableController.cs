using Microsoft.AspNetCore.Mvc;
using NaraWebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NaraWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TableController : ControllerBase
    {
        private readonly ITableManager _tableManager;
        public TableController(ITableManager tableManager)
        {
            _tableManager = tableManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMenuItem([FromBody] Data.Entities.Table table)
        {
            var newTable = await _tableManager.AddNewTable(table);

            return Ok(newTable);
        }
    }
}
