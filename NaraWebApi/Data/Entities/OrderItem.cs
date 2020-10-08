using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NaraWebApi.Data.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }

        public Menu Item { get; set; }

        public ICollection<AddOn> AddOns { get; set; }

        public Order Order { get; set; }

        public string comment { get; set; }

        public int  quantity { get; set; }
    }
}
