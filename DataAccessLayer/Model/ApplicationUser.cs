using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }

      //  public List<Order> Orders { get; set; } 

    }
}
