using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class UserDTO
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public int Age { get; set; }

        public List<UserDTO> Friends;

        public string Role { get; set; }
    }
}
