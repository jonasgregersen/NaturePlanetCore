using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferLayer.Model
{
    public class OrderDto
    {
        public int OrderNumber { get; set; }

        public virtual List<ProductDto> Products { get; set; } 

        public string UserId { get; set; } 
     
        
        public OrderDto()
        {
            
        }
    }
}
