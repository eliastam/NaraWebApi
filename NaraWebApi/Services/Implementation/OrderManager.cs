using NaraWebApi.Data;
using NaraWebApi.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NaraWebApi.DTO;
using System.Collections.ObjectModel;

namespace NaraWebApi.Services.Implementation
{
    public class OrderManager : IOrderManager
    {
        private readonly NaraContext _db;
        private readonly IOrderConverter _orderConverter;

        public OrderManager(NaraContext db, IOrderConverter orderConverter)
        {
            _db = db;
            _orderConverter = orderConverter;
        }

        public async Task<Menu> GetMenuItem(string name)
        {

            var baseQuery = _db.Menu.AsQueryable();
            //baseQuery = baseQuery.Where((e) => String.Equals(e.key,name, StringComparison.OrdinalIgnoreCase));
            baseQuery = baseQuery.Where((e) => string.Equals(e.ItemName, name));
            var entities = await baseQuery.ToListAsync();
            return entities.FirstOrDefault();
        }

        public async Task<ContentOrder> MakeAnOrder(ContentOrder contentOrder)
        {

            var order = new Order
            {
                Table = await GetTableByKey(contentOrder.Table),
                Comments = contentOrder.Comment,
                owner = contentOrder.Owner,
                OrderItems = await ConvertContentOrderItemToOrderItem(contentOrder.OrderItems)
            };
            _db.Orders.Add(order);

            await UpdateStoreStatus(order);
            await _db.SaveChangesAsync();

            return await _orderConverter.ConvertOrderToContentOrder(order);
        }

        public async Task<Table> GetTableByKey(string key)
        {
            var baseQuery = _db.Tables.AsQueryable();
            //baseQuery = baseQuery.Where((e) => String.Equals(e.key,name, StringComparison.OrdinalIgnoreCase));
            baseQuery = baseQuery.Where((e) => string.Equals(e.key, key));
            var tables = await baseQuery.ToListAsync();
            return tables.FirstOrDefault();
        }

        public async Task<AddOn> GetAddOnByName(string name)
        {
            var baseQuery = _db.AddOnsMenu.AsQueryable();
            //baseQuery = baseQuery.Where((e) => String.Equals(e.key,name, StringComparison.OrdinalIgnoreCase));
            baseQuery = baseQuery.Where((e) => string.Equals(e.ItemName, name));
            var addOnsItems = await baseQuery.ToListAsync();
            var addOn = new AddOn
            {
                AddOnItem = addOnsItems.FirstOrDefault()
            };
            return addOn;
        }
        public async Task<Menu> GetMenuItemByName(string name)
        {
            var baseQuery = _db.Menu.AsQueryable();
            //baseQuery = baseQuery.Where((e) => String.Equals(e.key,name, StringComparison.OrdinalIgnoreCase));
            baseQuery = baseQuery.Where((e) => string.Equals(e.ItemName, name));
            var MenuItems = await baseQuery.ToListAsync();

            return MenuItems.FirstOrDefault();
        }

        public async Task<ICollection<OrderItem>> ConvertContentOrderItemToOrderItem(IEnumerable<ContentOrderItem> contntOrderItems)
        {
            var output = new Collection<OrderItem>();

            foreach (var contentOrderItem in contntOrderItems)
            {
                var AddOns = new Collection<AddOn>();
                if (contentOrderItem.AddOns != null)
                {
                    foreach (var AddOn in contentOrderItem.AddOns)
                    {
                        AddOns.Add(await GetAddOnByName(AddOn));
                    }
                }
                var orderItem = new OrderItem
                {
                    comment = contentOrderItem.comment,
                    AddOns = AddOns,
                    Item = await GetMenuItemByName(contentOrderItem.ItemName),
                    quantity = contentOrderItem.quantity

                };
                output.Add(orderItem);
            }
            return output;
        }

        public async Task UpdateStoreStatus(Order order)
        {
            var dico = new Dictionary<string, int>();
            foreach (var orderItem in order.OrderItems)
            {

                var components = orderItem.Item.components;
                components = components.Replace(" ", "");
                var componentswithQuantity = components.Split(",");
                foreach (var componentwithQuantity in componentswithQuantity)
                {
                    var nameAndQuantity = componentwithQuantity.Split(":");
                    if (dico.ContainsKey(nameAndQuantity[0]))
                    {
                        var num = dico[nameAndQuantity[0]];
                        dico[nameAndQuantity[0]] = num + orderItem.quantity * Int16.Parse(nameAndQuantity[1]);
                    }
                    else
                    {
                        dico.Add(nameAndQuantity[0], orderItem.quantity * Int16.Parse(nameAndQuantity[1]));
                    }
                }
                foreach (var addOn in orderItem.AddOns)
                {
                    var storage = _db.StoreItmes.AsQueryable();

                    storage = storage.Where(e => e.ItemName == addOn.AddOnItem.ItemName);

                    var storeItems = await storage.ToListAsync();

                    if (storeItems != null)
                    {
                        var storeItem = storeItems.FirstOrDefault();

                        storeItem.Quantity = storeItem.Quantity - addOn.AddOnItem.Quantity;
                    }

                }
            }

            foreach (var item in dico)
            {
                var storage = _db.StoreItmes.AsQueryable();

                storage = storage.Where(e => e.ItemName == item.Key);

                var storeItems = await storage.ToListAsync();

                if (storeItems != null)
                {
                    var storeItem = storeItems.FirstOrDefault();

                    storeItem.Quantity = storeItem.Quantity - item.Value;
                }
            }
            await _db.SaveChangesAsync();


        }

        public async Task<IEnumerable<ContentOrder>> GetOrders(IEnumerable<string> TableKey, IEnumerable<string> owner)
        {
            var baseQuery = _db.Orders
                .Include((e) => e.OrderItems)
                .Include((e) => e.OrderItems).ThenInclude((e) => e.Item)
                .Include((e) => e.OrderItems).ThenInclude((e) => e.AddOns).ThenInclude((e) => e.AddOnItem)
                .Include((e) => e.Table)
                .AsQueryable();


            if (TableKey.Any())
            {
                baseQuery = baseQuery.Where((e) => TableKey.Contains(e.Table.key));
            }


            var orders = await baseQuery.ToListAsync();
            //var order = orders.FirstOrDefault();



            var list = new List<ContentOrder>();

            foreach (var order in orders)
            {
                var c = await _orderConverter.ConvertOrderToContentOrder(order);
                list.Add(c);
            }



            return list;
        }

        public async Task<IEnumerable<ContentOrderItem>> GetTableOrderItems(string TableKey)
        {
            var baseQuery = _db.Orders
                .Include((e) => e.OrderItems)
                .Include((e) => e.OrderItems).ThenInclude((e) => e.Item)
                .Include((e) => e.OrderItems).ThenInclude((e) => e.AddOns).ThenInclude((e) => e.AddOnItem)
                .Include((e) => e.Table)
                .AsQueryable();

            baseQuery = baseQuery.Where((e) => TableKey.Contains(e.Table.key));


            var orders = await baseQuery.ToListAsync();
            //var order = orders.FirstOrDefault();



            var list = new List<ContentOrderItem>();

            foreach (var order in orders)
            {
                var c = await _orderConverter.ConvertOrderToContentOrder(order);
                var cItems = c.OrderItems;
                foreach (var cItem in cItems)
                {
                    var s = list.Where((e) => e.ItemName == cItem.ItemName && e.AddOns.ToHashSet().SetEquals(cItem.AddOns.ToHashSet()));
                    if (s.Any())
                    {
                        var contentItem = s.FirstOrDefault();
                        var oldQuantity = contentItem.quantity;
                        contentItem.quantity = oldQuantity + cItem.quantity;
                    }
                    else 
                    {
                        list.Add(cItem); 
                    }
                }
               
            }



            return list;
        }
    }
}
