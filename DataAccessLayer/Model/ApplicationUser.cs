using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOOrder = DataTransferLayer.Model.Order;

namespace DataAccessLayer.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }

        public List<DTOOrder> Orders { get; set; } 

    }
}
