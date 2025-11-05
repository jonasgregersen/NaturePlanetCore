using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOProduct = DataTransferLayer.Model.Product;
using DALProduct = DataAccessLayer.Model.Product;

namespace DataAccessLayer.Mappers
{
    public class ProductMapper
    {

        public static DTOProduct Map(DALProduct product)
        {
            if (product != null)
            {
                return new DTOProduct(product.Name, product.EAN, product.ErpSource, product.Active, product.QuantityInBag,
                                      product.Weight, product.Segment, product.ProductCategory1, product.ProductCategory2,
                                      product.ProductCategory3);
            }
            return null;
        }

        public static DALProduct Map(DTOProduct product)
        {
            if (product != null)
            {
                return new DALProduct(product.Name, product.EAN, product.ErpSource, product.Active, product.QuantityInBag,
                                      product.Weight, product.Segment, product.ProductCategory1, product.ProductCategory2,
                                      product.ProductCategory3);
            }
            return null;
        }
    }
}
