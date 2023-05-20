using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.OrderAggregate
{
    public class DeliveryMethod:BaseEntity
    {
        public string ShortName { get; set; }

        public string DeliveryTime { get; set; }

        public string Descripton { get; set; }

        public decimal Price { get; set; }
    }
}
