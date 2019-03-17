using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SocialNetworkMvc.Models
{
    public class RegisterModel
    {
       
        public string Email { get; set; }
        
        public string Password { get; set; }
        
        public string ConfirmPassword { get; set; }
        
        public System.Web.Mvc.SelectList Country { get; set; }
       
       
        
        public string Name { get; set; }
        
      
        public int Age { get; set; }



    }
}