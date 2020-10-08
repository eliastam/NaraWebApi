using NaraWebApi.Data.Entities;
using NaraWebApi.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NaraWebApi.Services
{
    public interface IOrderConverter
    {
        public Task<ContentOrder> ConvertOrderToContentOrder(Order order);
        public IEnumerable<ContentOrderItem> ConvertOrderItemToContentOrderItem(ICollection<OrderItem> orderItems);
    }
}
