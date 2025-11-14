using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model
{
    public class Order
    {
        public int OrderNumber { get; set; }    

        public virtual List<Product> Products { get; set; } = new List<Product>();

        public Order(int órderNumber, List<Product> products)
        {
            OrderNumber = OrderNumber;
            Products = products;
        }

        public Order() { }
    }
}
