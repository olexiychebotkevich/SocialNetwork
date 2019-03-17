using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SocialNetworkMvc.Models
{
    public class GroupsModel
    {
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Name { get; set; }

        public string MainImage { get; set; }
    }
}