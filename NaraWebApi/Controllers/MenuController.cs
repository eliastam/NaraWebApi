using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NaraWebApi.Data.Entities;
using NaraWebApi.Services;

namespace NaraWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MenuController : ControllerBase
    {
        private readonly IMenuManager _menuManager;
        public MenuController(IMenuManager menuManager)
        {
            _menuManager = menuManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMenuItem([FromBody] Menu menu)
        {
            var menuItem = await _menuManager.AddItemToMenu(menu);
            return Ok(menuItem);
        }
        [HttpGet]
        public async Task<IActionResult> GetMenuItem([FromQuery(Name = "ItemName")] IEnumerable<string> Names, [FromQuery(Name = "Types")] IEnumerable<string> types)
        {
            var output = await _menuManager.GetMenuItems(Names, types);
            return Ok(output);
        }

        [HttpPut("{MenuItemName}")]
        public async Task<IActionResult> UpdateMenuItem([FromRoute] string MenuItemName, [FromBody] Menu menu)
        {
            if (! await _menuManager.IsExsistedMenu(MenuItemName))
            {
                return BadRequest($"No MenuItem found under the key '{MenuItemName}'.");
            }
            else
            {
                var menuItem = await _menuManager.UpdateMenuItem(MenuItemName, menu);
                return Ok(menuItem);
            }
            
        }
        [HttpDelete("{MenuItemName}")]
        public async Task<IActionResult> DeleteMenuItem([FromRoute] string MenuItemName)
        {
            if (!await _menuManager.IsExsistedMenu(MenuItemName))
            {
                return BadRequest($"No MenuItem found under the key '{MenuItemName}'.");
            }
            else
            {
                await _menuManager.DeleteMenuItem(MenuItemName);
                return Ok();
            }

        }
    }
}