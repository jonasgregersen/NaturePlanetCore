using System;
using System.Collections.Generic;

namespace Business.Model
{
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int AuthenticationLevel { get; set; }
        public List<Order> OrderHistory { get; set; } = new List<Order>();
    }
}
