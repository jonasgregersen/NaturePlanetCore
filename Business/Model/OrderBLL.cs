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
        public List<ProductBLL> Products { get; set; } = new List<ProductBLL>();

        public double GetTotalPrice()
        {
            double total = 0;
            foreach (ProductBLL product in Products)
            {
                total += product.getPrice();
            }
            return total;
        }

        public List<ProductBLL> GetProducts()
        {
            return new List<ProductBLL>(Products);
        }
    }
}
