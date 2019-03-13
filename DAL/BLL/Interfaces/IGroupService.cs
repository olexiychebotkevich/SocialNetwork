using BLL.DTO;
using BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IGroupService:IDisposable
    {
        OperationDetails Create(GroupDTO grouprDto);

        List<GroupDTO> GetGroups();

        GroupDTO GetGroup(string Name);
    }
}
