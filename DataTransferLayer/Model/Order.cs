using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferLayer.Model
{
    public class Order
    {
        public int OrderNumber { get; set; }

        public virtual List<Product> Products { get; set; } 

        public Order(int orderNumber, List<Product> products)
        {
            OrderNumber = orderNumber;
            Products = products;
        }

        public Order(int orderNumber)
        {
            OrderNumber = orderNumber;
        }
    }
}
