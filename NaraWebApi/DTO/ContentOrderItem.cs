using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NaraWebApi.DTO
{
    public class ContentOrderItem
    {
        public string ItemName{ get; set; }

        public IEnumerable<string> AddOns { get; set; }

        public string comment { get; set; }

        public int quantity { get; set; }

        public int Paid { get; set; }
    }
}
