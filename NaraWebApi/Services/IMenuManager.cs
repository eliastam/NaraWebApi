using NaraWebApi.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NaraWebApi.Services
{
   public interface IMenuManager
    {
        public Task<Menu> AddItemToMenu(Menu menu);
        public Task<IEnumerable<Menu>> GetMenuItems(IEnumerable<string> name, IEnumerable<string> types);
        public Task<Menu> UpdateMenuItem(string menuItemName, Menu menu);
        public Task DeleteMenuItem(string menuItemName);
        public Task<bool> IsExsistedMenu(string menuItemName);

        public Task<AddOnMenu> AddItemToAddOnMenu(AddOnMenu menu);
        public Task<IEnumerable<AddOnMenu>> GetAddOnMenuItems(IEnumerable<string> name);
        public Task<AddOnMenu> UpdateAddOnMenuItem(string menuItemName, AddOnMenu menu);
        public Task DeleteAddOnMenuItem(string menuItemName);
        public Task<bool> IsExsistedAddOnMenu(string menuItemName);
    }
}
