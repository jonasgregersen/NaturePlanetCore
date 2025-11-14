using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Model;
using DTOProduct = DataTransferLayer.Model.Product;

namespace Business.Model
{
    public class OrderBLL
    {
        public int OrderNumber { get; set; }
        public List<DTOProduct> Products { get; set; } = new List<DTOProduct>();
        

        public List<DTOProduct> GetProducts()
        {
            return new List<DTOProduct>(Products);
        }

        public void AddProduct(DTOProduct product)
        {
            Products.Add(product);
        }
    }
}
