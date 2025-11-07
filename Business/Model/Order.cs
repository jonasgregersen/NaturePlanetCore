using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Model;

namespace Business.Model
{
    public class Order
    {
        public int OrderNumber { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();

        public double GetTotalPrice()
        {
            double total = 0;
            foreach (Product product in Products)
            {
                total += product.getPrice();
            }
            return total;
        }

        public List<Product> GetProducts()
        {
            return new List<Product>(Products);
        }
    }
}
