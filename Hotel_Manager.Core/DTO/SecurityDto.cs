using Hotel_Manager.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Manager.Core.DTO
{
    public class SecurityDto
    {
        public string Name { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public RoleType? Role { get; set; }
    }
}
