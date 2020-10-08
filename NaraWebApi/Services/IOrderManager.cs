using NaraWebApi.Data.Entities;
using NaraWebApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NaraWebApi.Services
{
    public interface IOrderManager
    {
         Task<ContentOrder> MakeAnOrder(ContentOrder contentOrder);
         Task<Menu> GetMenuItem(string name);
    }
}
