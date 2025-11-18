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

        public ApplicationUser? User { get; set; }

        public string? UserId { get; set; }
        public virtual List<DALProduct> Products { get; set; } = new List<DALProduct>();


        public Order() { }
        public Order(int orderNumber, List<DALProduct> products)
        {
            OrderNumber = orderNumber;
            Products = products;
        }

        
    }
}
