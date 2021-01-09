using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NaraWebApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NaraWebApi.Services.Implementation
{
    public class TableManager : ITableManager
    {
        private readonly NaraContext _db;


        public TableManager(NaraContext db)
        {
            _db = db;

        }
        public async  Task<Data.Entities.Table> AddNewTable(Data.Entities.Table table)
        {
            // verification
            if (table.key != null)
            {
                _db.Tables.Add(table);
                await _db.SaveChangesAsync();
            }
            // _hubContext.Clients.All.SendAsync("ss");
            return table;
        }

        public Task DeleteTableItem(string menuItemName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Table>> GetMenuItems(IEnumerable<string> name, IEnumerable<string> types)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsExsistedMenu(string menuItemName)
        {
            throw new NotImplementedException();
        }

        public Task<Table> UpdateTableItem(string menuItemName, Table menu)
        {
            throw new NotImplementedException();
        }
    }
}
