using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataTransferLayer.Model
{
    public class ProductDto
    {
        [Required(ErrorMessage = "Product ID is required.")]
        public string Id { get; set; }
        [Required(ErrorMessage = "Produktet skal have et navn.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Produktet skal have en EAN.")]
        public string EAN { get; set; }
        [Required(ErrorMessage = "Produktet skal have en ERP source.")]
        public string ErpSource { get; set; }
        public bool Active { get; set; }
        [Required(ErrorMessage = "Produktet skal have en antal i bag.")]
        public int QuantityInBag { get; set; }
        [Required(ErrorMessage = "Produktet skal have en vægt.")]
        public decimal Weight { get; set; }
        [Required(ErrorMessage = "Produktet skal have en segment.")]
        public string? Segment { get; set; }
        [Required(ErrorMessage = "Produktet skal have en kategori 1.")]
        public string Product_Category_1 { get; set; }
        [Required(ErrorMessage = "Produktet skal have en kategori 2.")]
        public string? Product_Category_2 { get; set; }
        [Required(ErrorMessage = "Produktet skal have en kategori 3.")]
        public string? Product_Category_3 { get; set; }
       

        public ProductDto() { }
    }
}
