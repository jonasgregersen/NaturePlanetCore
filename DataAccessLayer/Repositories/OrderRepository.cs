using DataAccessLayer.Context;
using DataAccessLayer.Model;
using DataTransferLayer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALOrder = DataAccessLayer.Model.Order;

namespace DataAccessLayer.Repositories
{
    public class OrderRepository
    {
        private readonly DatabaseContext _context;

        public OrderRepository(DatabaseContext context)
        {
            _context = context;
        }

        
        public DALOrder GetOrder(int id)
        {
            return _context.Orders.FirstOrDefault(o => o.OrderId == id);
        }

        public List<DALOrder> GetAllOrders()
        {
            return _context.Orders.ToList();
        }

        public List<DALOrder> GetAllOrdersForUser(string id)
        {


            return _context.Orders.Where(o =>  o.UserId == id).
                Include(o => o.Products).ToList();

        }

        public void AddOrderToUser(string id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public DALProduct GetProductById(string id)
        {
            return _context.Products.FirstOrDefault(p => p.ProductId == id);
        }


        public void AddOrder(Order order)
        {
            if (order == null) 
                throw new ArgumentNullException(nameof(order));

            // Create a copy of the products list to avoid modification during iteration
            var productsToProcess = order.Products.ToList();
            order.Products.Clear(); // Clear the original list

            // First, attach the order without tracking the products
            _context.Orders.Add(order);

            // Handle each product in the order
            foreach (var product in productsToProcess)
            {
                // Check if the product is already being tracked
                var existingProduct = _context.Products
                    .Local
                    .FirstOrDefault(p => p.ProductId == product.ProductId);

                if (existingProduct != null)
                {
                    // If the product is already tracked, use the tracked instance
                    order.Products.Add(existingProduct);
                }
                else
                {
                    // If not tracked, attach it
                    _context.Products.Attach(product);
                    order.Products.Add(product);
                }
            }

            _context.SaveChanges();
        }
        
    }
}
