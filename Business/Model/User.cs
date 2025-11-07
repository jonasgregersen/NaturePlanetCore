using System;
using System.Collections.Generic;

namespace Business.Model
{
    public class User
    {
        public string name { get; set; }
        public string email { get; set; }
        public int authenticationLevel { get; set; }
        public List<Order> orderHistory { get; set; } = new List<Order>();
    }
}
