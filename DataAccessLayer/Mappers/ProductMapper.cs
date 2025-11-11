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
                return new DTOProduct(product.Name, product.EAN, product.ERP_Source, product.Active, product.Purchase_quantity_step,
                                      product.Weight, product.Segment, product.Product_Category_1, product.Product_Category_2,
                                      product.Product_Category_3);
            }
            return null;
        }

        public static DALProduct Map(DTOProduct product)
        {
            if (product != null)
            {
                return new DALProduct(product.Name, product.EAN, product.ErpSource, product.Active, product.QuantityInBag,
                                      product.Weight, product.Segment, product.Product_Category_1, product.Product_Category_2,
                                      product.Product_Category_3);
            }
            return null;
        }
    }
}
