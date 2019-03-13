using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IGroupManager: IDisposable
    {
        void Create(Group item);

        List<Group> GetAllGroups();

        Group GetGroupByName(string Name);

        
    }
}
