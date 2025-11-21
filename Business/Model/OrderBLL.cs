using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Model;
using DataAccessLayer.Mappers;
using DataAccessLayer.Model;
using DataAccessLayer.Repositories;
using DataTransferLayer.Model;
using DTOProduct = DataTransferLayer.Model.ProductDto;

namespace Business.Model
{
    public class OrderBLL
    {
        public int OrderNumber { get; set; }
        public List<DTOProduct> Products { get; set; } = new List<DTOProduct>();
        private readonly OrderRepository _orderRepository;

        public OrderBLL(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }


        public List<DTOProduct> GetProducts()
        {
            return new List<DTOProduct>(Products);
        }

        public void AddProduct(DTOProduct product)
        {
            Products.Add(product);
        }
        
        
        public List<OrderDto> GetUserOrders(string id)
        {
            return _orderRepository.GetAllOrdersForUser(id)
                .Select(o => OrderMapper.MapToDto(o))
                .ToList();
        }
        public void CreateOrder(OrderDto orderDto)
        {
            var dalOrder = OrderMapper.Map(orderDto);

            dalOrder.UserId = orderDto.UserId;

            foreach (var dtoProduct in orderDto.Products)
            {
                var product = _orderRepository.GetProductById(dtoProduct.Id.ToString());

                if (product != null)
                    dalOrder.Products.Add(product);
            }

            _orderRepository.AddOrder(dalOrder);
        }

    }
}
