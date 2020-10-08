using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NaraWebApi.Data.Entities
{
    public class Table
    {
        public int Id { get; set; }

        public string key { get; set; }

        public string type { get; set; }

        public ICollection<Order> Orders { get; set; }
}
}
