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
    public class GroupManager: IGroupManager
    {
        public ApplicationContext Database { get; set; }
        public GroupManager(ApplicationContext db)
        {
            Database = db;
        }

        public void Create(Group item)
        {
            Database.Groups.Add(item);
            Database.SaveChanges();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public Group GetGroupByName(string Name)
        {
            Group group;
            try
            {
                group = Database.Groups.Single(x => x.Name == Name);
            }
            catch
            {
                group = null;
            }

            return group;
        }


        public List<Group> GetAllGroups()
        {
            return Database.Groups.ToList();
        }
    }
}
