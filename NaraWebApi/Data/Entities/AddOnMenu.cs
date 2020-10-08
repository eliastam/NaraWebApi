using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NaraWebApi.Data.Entities
{
    public class AddOnMenu
    {
        public int Id { get; set; }

        public string ItemName { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }
    }
}
