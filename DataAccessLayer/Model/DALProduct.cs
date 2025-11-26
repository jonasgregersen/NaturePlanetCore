using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace DataAccessLayer.Model
{
    public class DALProduct
    {
        [Key]
        [Column("ProductId")]  // ← matcher DB
        public string ProductId { get; set; } = string.Empty;

        [Column("Name")]
        public string Name { get; set; } = string.Empty;

        [Column("EAN")]
        public string EAN { get; set; } = string.Empty;

        [Column("ERP_Source")]
        public string ERP_Source { get; set; } = string.Empty;

        [Column("Active")]
        public bool Active { get; set; } = true;

        [Column("Purchase_quantity_step")]
        public int Purchase_quantity_step { get; set; } = 0;

        [Column("Weight")]
        public decimal Weight { get; set; } = 0m;

        [Column("Segment")]
        public string Segment { get; set; } = string.Empty;

        // MATCH DB: Product_Category_1
        [Column("Product_Category_1")]
        public string Product_Category_1 { get; set; } = string.Empty;

        [Column("Product_Category_2")]
        public string Product_Category_2 { get; set; } = string.Empty;

        [Column("Product_Category_3")]
        public string Product_Category_3 { get; set; } = string.Empty;

        public virtual List<Order> Orders { get; set; } = new();

        public DALProduct() { }
       
    }
}