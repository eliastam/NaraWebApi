using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NaraWebApi.Data.Entities
{
    public class AddOn
    {
        public int Id { get; set; }

        public AddOnMenu AddOnItem { get; set; }

        public OrderItem OrderItem { get; set; }

        public string comment { get; set; }

        public int quantity { get; set; }
    }
}
