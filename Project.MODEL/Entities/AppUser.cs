using Project.MODEL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MODEL.Entities
{
    public class AppUser:BaseEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public UserRole? Role { get; set; }

        //RelationalProperties
        public virtual AppUserDetail AppUserDetail { get; set; }
        public virtual List<Order> Orders { get; set; }
    }
}
