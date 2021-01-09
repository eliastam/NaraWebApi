using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NaraWebApi.Services
{
    public interface ITableManager
    {
        public Task<Data.Entities.Table> AddNewTable(Data.Entities.Table table);
        public Task<IEnumerable<Table>> GetMenuItems(IEnumerable<string> name, IEnumerable<string> types);
        public Task<Table> UpdateTableItem(string menuItemName, Table menu);
        public Task DeleteTableItem(string menuItemName);
        public Task<bool> IsExsistedMenu(string menuItemName);

    }
}
