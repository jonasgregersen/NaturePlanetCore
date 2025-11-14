using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOOrder = DataTransferLayer.Model.Order;
using DALOrder = DataAccessLayer.Model.Order;
using DTOProduct = DataTransferLayer.Model.Product;
using DALProduct = DataAccessLayer.Model.Product;

using DataTransferLayer.Model;
using DataAccessLayer.Model;


namespace DataAccessLayer.Mappers
{
    public class OrderMapper
    {
        public static DTOOrder Map(DALOrder order)
        {
            if (order != null)
            {
                var _products = new List<DTOProduct>();
                var products = order.Products;
                foreach(var product in products)
                {
                    _products.Add(ProductMapper.Map(product));
                    
                }
                return new DTOOrder(order.OrderNumber, _products);
            }
            return null;
        }

        public static DALOrder Map(DTOOrder order)
        {
            if (order != null)
            {
                var _products = new List<DALProduct>();
                var products = order.Products;
                foreach (var product in products)
                {
                    _products.Add(ProductMapper.Map(product));

                }
                return new DALOrder(order.OrderNumber, _products);
            }
            return null;
        }
    }
}
