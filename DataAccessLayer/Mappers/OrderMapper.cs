using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Model;
using DataTransferLayer.Model;

namespace DataAccessLayer.Mappers
{
    public static class OrderMapper
    {
        public static Order Map(OrderDto orderDto)
        {
            return new Order
            {
                OrderNumber = orderDto.OrderNumber,
                // Products udfyldes IKKE her
                Products = new List<DALProduct>()
            };
        }


        public static OrderDto MapToDto(Order order)
        {
            if (order?.Products == null)
                return new OrderDto{OrderNumber = 0, Products = new List<ProductDto>()};

            var productDtos = order.Products
                .Where(p => p != null)
                .Select(p => new ProductDto(
                    name: p.Name,
                    ean: p.EAN,
                    erpSource: p.ERP_Source,
                    active: p.Active,
                    quantityInBag: 1,  
                    weight: p.Weight,
                    segment: string.Empty,  
                    productCategory1: string.Empty,
                    productCategory2: string.Empty,
                    productCategory3: string.Empty
                ))
                .ToList();

            return new OrderDto
            {
                OrderNumber = order.OrderNumber,
                Products = productDtos
            };


        }
    }
}