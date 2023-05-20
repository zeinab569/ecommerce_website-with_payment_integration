using Core.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class OrderWithItemsAndOrdringSpecification:BaseSpecification<Order>
    {
        public OrderWithItemsAndOrdringSpecification(string email):base(o=>o.BuyerEmail==email)
        {
            AddIncludes(o => o.Items);
            AddIncludes(o => o.deliveryMethod);
            AddIncludes(o => o.OrderDate);
        }

        public OrderWithItemsAndOrdringSpecification(int id,string email):base(o=>o.Id == id && o.BuyerEmail==email)
        {
            AddIncludes(o => o.Items);
            AddIncludes(o => o.deliveryMethod);
            AddIncludes(o => o.OrderDate);
        }
    }
}
