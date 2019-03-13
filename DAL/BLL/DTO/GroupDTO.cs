using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class GroupDTO
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual List<UserDTO> Subscribers { get; set; }

        public virtual List<PostDTO> Posts { get; set; }
    }
}
