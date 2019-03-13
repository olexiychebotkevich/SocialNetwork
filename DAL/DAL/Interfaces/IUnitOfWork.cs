using DAL.Identity;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ApplicationUserManager UserManager { get; }

        IClientManager ClientManager { get; }
        
        IGroupManager GroupManager { get; }

        IPostManager PostManager { get; }

        ApplicationRoleManager RoleManager { get; }

        Task SaveAsync();
    }
}
