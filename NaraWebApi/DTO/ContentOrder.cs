using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NaraWebApi.DTO
{
    public class ContentOrder
    {
        public string Table { get; set; }

        public string Comment { get; set; }

        public string Owner { get; set; }

        public IEnumerable<ContentOrderItem> OrderItems {get ;set ;}

        public DateTime date { get; set;  }

    }
}
