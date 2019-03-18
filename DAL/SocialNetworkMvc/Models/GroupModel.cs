using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetworkMvc.Models
{
    public class GroupModel
    {
        
        public string Subject { get; set; }
       
        public string Description { get; set; }
       
        public string Name { get; set; }
       
        public string Author { get; set; }

        public string MainImage { get; set; }
    }
}