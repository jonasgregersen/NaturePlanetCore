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
                UserId = orderDto.UserId,
                Products = new List<DALProduct>(),
                OrderDate = orderDto.OrderDate
            };
        }


        public static OrderDto MapToDto(Order order)
        {
            if (order?.Products == null)
                return new OrderDto{OrderNumber = 0, Products = new List<ProductDto>()};

            var productDtos = order.Products
                .Where(p => p != null)
                .Select(p => new ProductDto
                {
                    Id = p.ProductId,
                    Name = p.Name,
                    EAN = p.EAN,
                    ErpSource = p.ERP_Source,
                    Active = p.Active,
                    QuantityInBag = 1,
                    Weight = p.Weight,
                    Segment = string.Empty,
                    Product_Category_1 = string.Empty,
                    Product_Category_2 = string.Empty,
                    Product_Category_3 = string.Empty
                })
                .ToList();
            return new OrderDto
            {
                OrderNumber = order.OrderNumber,
                Products = productDtos,
                UserId = order.UserId,
                OrderDate = order.OrderDate
            };


        }
    }
}