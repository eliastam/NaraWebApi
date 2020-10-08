using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NaraWebApi.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }

        public string owner { get; set; }

        public string Comments { get; set; }

        public Table Table { get; set; }

    }
}
