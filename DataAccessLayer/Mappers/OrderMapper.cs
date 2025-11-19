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
            if (orderDto?.Products == null) 
                return new Order { Products = new List<DALProduct>() };

            var order = new Order
            {
                OrderNumber = orderDto.OrderNumber,
                Products = orderDto.Products
                    .Where(p => p != null)
                    .Select(p => new DALProduct
                    {
                        ProductId = p.Id.ToString(),  // Convert to string as per DALProduct
                        Name = p.Name,
                        EAN = p.EAN,
                        ERP_Source = p.ErpSource,
                        Active = p.Active,
                        Weight = p.Weight ?? 0m
                        // Note: You might need to map additional properties
                    })
                    .ToList()
            };

            return order;
        }

        public static OrderDto MapToDto(Order order)
        {
            if (order?.Products == null)
                return new OrderDto(0, new List<ProductDto>());

            var productDtos = order.Products
                .Where(p => p != null)
                .Select(p => new ProductDto(
                    name: p.Name,
                    ean: p.EAN,
                    erpSource: p.ERP_Source,
                    active: p.Active,
                    quantityInBag: 1,  // Default value or get from somewhere
                    weight: p.Weight,
                    segment: string.Empty,  // Default value or get from somewhere
                    productCategory1: string.Empty,
                    productCategory2: string.Empty,
                    productCategory3: string.Empty
                ))
                .ToList();

            return new OrderDto(
                orderNumber: order.OrderNumber,
                products: productDtos
            );
        }
    }
}