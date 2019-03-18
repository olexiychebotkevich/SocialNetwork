using System;
using System.Collections.Generic;

namespace BLL.DTO
{
    public class PostDTO
    {
        public int ID { get; set; }

        public string Subject { get; set; }

        public string Text { get; set; }

        public string Photo { get; set; }

        public string Author { get; set; }

        public DateTime Date { get; set; }
    }
}