using NaraWebApi.Data.Entities;
using NaraWebApi.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace NaraWebApi.Services.Implementation
{
    public class OrderConverter : IOrderConverter
    {
        public async Task<ContentOrder> ConvertOrderToContentOrder(Order order)
        {
            var output = new ContentOrder
            {
                Table = order.Table.key,
                Comment = order.Comments,
                date = DateTime.Now,
                Owner = "Nara",
                OrderItems = ConvertOrderItemToContentOrderItem(order.OrderItems)
            };

            return output;
        }

        public IEnumerable<ContentOrderItem> ConvertOrderItemToContentOrderItem(ICollection<OrderItem> orderItems)
        {
            var output = new List<ContentOrderItem>();
            foreach(var orderItem in orderItems)
            {
                var AddOnNames = new List<string>();
                if (orderItem.AddOns!= null)
                { 
                    foreach (var AddOn in orderItem.AddOns)
                    {
                        AddOnNames.Add(AddOn.AddOnItem.ItemName);
                    }
                }
              
                var contentOrderItem = new ContentOrderItem
                {
                    AddOns = AddOnNames,
                    comment = orderItem.comment,
                    ItemName = orderItem.Item.ItemName,
                    quantity = orderItem.quantity,
                    Paid     = orderItem.Paid
                };

                output.Add(contentOrderItem);
            }
            return output;
        }

       
    }
}
