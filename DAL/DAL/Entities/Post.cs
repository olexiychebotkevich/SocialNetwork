using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Post
    {
        [Key]
        public int ID { get; set; }

        public string Subject { get; set; }

        public string Text { get; set; }

        public string Photo { get; set; }

        public string Author { get; set; }

        public DateTime Date { get; set; }
    }
}