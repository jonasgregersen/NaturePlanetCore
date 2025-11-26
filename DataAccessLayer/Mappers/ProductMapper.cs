using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOProduct = DataTransferLayer.Model.ProductDto;
using DALProduct = DataAccessLayer.Model.DALProduct;

namespace DataAccessLayer.Mappers
{
    public class ProductMapper
    {

        public static DTOProduct Map(DALProduct product)
        {
            if (product != null)
            {
                return new DTOProduct
                {
                    Id = product.ProductId,
                    Name = product.Name,
                    EAN = product.EAN,
                    ErpSource = product.ERP_Source,
                    Active = product.Active,
                    QuantityInBag = product.Purchase_quantity_step,
                    Weight = product.Weight,
                    Segment = product.Segment,
                    Product_Category_1 = product.Product_Category_1,
                    Product_Category_2 = product.Product_Category_2,
                    Product_Category_3 = product.Product_Category_3,
                };
            }
            return null;
        }

        public static DALProduct Map(DTOProduct product)
        {
            if (product != null)
            {
                return new DALProduct
                {
                    ProductId = product.Id,
                    Name = product.Name,
                    EAN = product.EAN,
                    ERP_Source = product.ErpSource,
                    Active = product.Active,
                    Purchase_quantity_step = product.QuantityInBag,
                    Weight = product.Weight,
                    Segment = product.Segment,
                    Product_Category_1 = product.Product_Category_1,
                    Product_Category_2 = product.Product_Category_2,
                    Product_Category_3 = product.Product_Category_3,
                };
            }
            return null;
        }
    }
}
