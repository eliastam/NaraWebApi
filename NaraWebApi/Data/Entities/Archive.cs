using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NaraWebApi.Data.Entities
{
    public class Archive
    {
        public int Id { get; set; }

        public string Date { get; set; } 

        public string Day { get; set; }
        
        public string ItemName { get; set; }
        
        public string ItemType { get; set; }
        public int Quantity { get; set; }

    }
}
