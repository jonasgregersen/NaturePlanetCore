using DataAccessLayer.Context;
using DataAccessLayer.Model;
using DataTransferLayer.Model;
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

        public void AddOrder(DALOrder order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }
        
    }
}
