using NaraWebApi.Data;
using NaraWebApi.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;


namespace NaraWebApi.Services.Implementation
{
    public class menuManager : IMenuManager
    {
        private readonly NaraContext _db;
       

        public menuManager(NaraContext db )
        {
            _db = db;
       
        }
        #region MainMenu

        public async Task<Menu> AddItemToMenu(Menu menu)
        {
            // verification
            if (menu.ItemName != null)
            {
                _db.Menu.Add(menu);
                await _db.SaveChangesAsync();
            }
           // _hubContext.Clients.All.SendAsync("ss");
            return menu;
        }
        public async Task<IEnumerable<Menu>> GetMenuItems(IEnumerable<string> names, IEnumerable<string> Types)
        {
            var baseQuery =  _db.Menu.AsQueryable();

            names = (names ?? Enumerable.Empty<string>())
                   .Where((s) => s != null)
                   .ToHashSet();

            if (names.Any() == true)
            {
                baseQuery = baseQuery.Where((e) => names.Contains(e.ItemName));
            }

            Types = (Types ?? Enumerable.Empty<string>())
                .Where((s) => s != null)
                .ToHashSet();

            if (Types.Any() == true)
            {
                baseQuery = baseQuery.Where((e) => Types.Contains(e.Type));
            }

            return await baseQuery.ToListAsync();
        }
        public async Task<Menu> GetMenuItem(string names)
        {
            var output = await GetMenuItems(new[] { names }, null);

            return output.FirstOrDefault();
        }
        public async Task<Menu> UpdateMenuItem (string menuItemName, Menu menu)
        {
            var existedMenu = await GetMenuItem(menuItemName);

            if (menu.components != null)
            {
                existedMenu.components = menu.components;
            }
            if (menu.Price != 0)
            {
                existedMenu.Price = menu.Price;
            }
            if (menu.Type != null)
            {
                existedMenu.Type = menu.Type;
            }

           
           

            await _db.SaveChangesAsync();
            return menu;
        }
        public async Task DeleteMenuItem(string menuItemName)
        {
            var menu =await  GetMenuItem(menuItemName);

            _db.Menu.Remove(menu);

            await _db.SaveChangesAsync();
        }
        public async Task<bool> IsExsistedMenu(string menuItemName)
        {
            return await _db.Menu.AnyAsync((e) => e.ItemName == menuItemName);
        }

        #endregion

        #region AddOnMenu
        public async Task<AddOnMenu> AddItemToAddOnMenu(AddOnMenu addOnMenu)
        {
            // verification
            if (addOnMenu.ItemName != null)
            {
                _db.AddOnsMenu.Add(addOnMenu);
                await _db.SaveChangesAsync();
            }
            return addOnMenu;
        }
        public async Task<IEnumerable<AddOnMenu>> GetAddOnMenuItems(IEnumerable<string> names)
        {
            var baseQuery = _db.AddOnsMenu.AsQueryable();

            names = (names ?? Enumerable.Empty<string>())
                   .Where((s) => s != null)
                   .ToHashSet();

            if (names.Any() == true)
            {
                baseQuery = baseQuery.Where((e) => names.Contains(e.ItemName));
            }

            return await baseQuery.ToListAsync();
        }
        public async Task<AddOnMenu> GetAddOnMenuItem(string names)
        {
            var output = await GetAddOnMenuItems(new[] { names });

            return output.FirstOrDefault();
        }
        public async Task<AddOnMenu> UpdateAddOnMenuItem(string menuItemName, AddOnMenu addOnMenu)
        {
            var existedAddOnMenu = await GetAddOnMenuItem(menuItemName);

            if (addOnMenu.Price != 0)
            {
                existedAddOnMenu.Price = addOnMenu.Price;
            }
            await _db.SaveChangesAsync();
            return addOnMenu;
        }
        public async Task DeleteAddOnMenuItem(string AddOnMenuItemName)
        {
            var addOnMenuItem = await GetAddOnMenuItem(AddOnMenuItemName);

            _db.AddOnsMenu.Remove(addOnMenuItem);

            await _db.SaveChangesAsync();
        }
        public async Task<bool> IsExsistedAddOnMenu(string addOnMenuItemName)
        {
            return await _db.AddOnsMenu.AnyAsync((e) => e.ItemName == addOnMenuItemName);
        }


        #endregion
    }
}
