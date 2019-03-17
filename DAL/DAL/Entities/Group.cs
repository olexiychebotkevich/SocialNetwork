using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Group
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual List<ClientProfile> Subscribers { get; set; }

        public virtual List<Post> Posts { get; set; }

        public string MainImage { get; set; }
    }
}
