using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NaraWebApi.Data.Entities;
using NaraWebApi.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NaraWebApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class AddOnMenuController : ControllerBase
    {
        private readonly IMenuManager _menuManager;
        public AddOnMenuController(IMenuManager menuManager)
        {
            _menuManager = menuManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAddOnMenuItem([FromBody] AddOnMenu addOnMenu)
        {
            var menuItem = await _menuManager.AddItemToAddOnMenu(addOnMenu);
            return Ok(menuItem);
        }
        [HttpGet]
        public async Task<IActionResult> GetAddOnMenuItems([FromQuery(Name = "ItemName")] IEnumerable<string> Names)
        {
            var output = await _menuManager.GetAddOnMenuItems(Names);
            return Ok(output);
        }

        [HttpPut("{addOnMenuItemName}")]
        public async Task<IActionResult> UpdateAddOnMenuItem([FromRoute] string addOnMenuItemName, [FromBody] AddOnMenu addOnMenu)
        {
            if (!await _menuManager.IsExsistedAddOnMenu(addOnMenuItemName))
            {
                return BadRequest($"No AddOnMenuItem found under the key '{addOnMenuItemName}'.");
            }
            else
            {
                var menuItem = await _menuManager.UpdateAddOnMenuItem(addOnMenuItemName, addOnMenu);
                return Ok(menuItem);
            }

        }
        
        [HttpDelete("{addOnMenuItemName}")]
        public async Task<IActionResult> DeleteAddOnMenuItem([FromRoute] string addOnMenuItemName)
        {
            if (!await _menuManager.IsExsistedAddOnMenu(addOnMenuItemName))
            {
                return BadRequest($"No AddOnMenuItem found under the key '{addOnMenuItemName}'.");
            }
            else
            {
                await _menuManager.DeleteAddOnMenuItem(addOnMenuItemName);
                return Ok();
            }

        }
    }
}
