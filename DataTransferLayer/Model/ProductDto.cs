using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataTransferLayer.Model
{
    public class ProductDto
    {

        public string Id { get; set; }
        public string Name { get; set; }
        public string? EAN { get; set; }
        public string ErpSource { get; set; }
        public bool Active { get; set; }
        public int? QuantityInBag { get; set; }
        public decimal? Weight { get; set; }
        public string Segment { get; set; }
        public string Product_Category_1 { get; set; }
        public string Product_Category_2 { get; set; }
        public string Product_Category_3 { get; set; }
       

        public ProductDto() { }
    }
}
