using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
   public class PostManager:IPostManager
    {

        public ApplicationContext Database { get; set; }
        public PostManager(ApplicationContext db)
        {
            Database = db;
        }

        public void Create(Post item)
        {
            Database.Posts.Add(item);
            Database.SaveChanges();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
