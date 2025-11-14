using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Model
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public int OrderNumber { get; set; }    

        public virtual List<Product> Products { get; set; } = new List<Product>();

        public Order(int orderNumber, List<Product> products)
        {
            OrderNumber = orderNumber;
            Products = products;
        }

        public Order() { }
    }
}
