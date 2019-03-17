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
    public class ClientManager : IClientManager
    {
        public ApplicationContext Database { get; set; }
        public ClientManager(ApplicationContext db)
        {
            Database = db;
        }

        public void Create(ClientProfile item)
        {
            Database.ClientProfiles.Add(item);
            Database.SaveChanges();
        }


        public List<ClientProfile>GetUsers()
        {
            return Database.ClientProfiles.ToList();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public ClientProfile GetUserByEmail(string Email)
        {
            ClientProfile user;
            try
            {
                user = Database.ClientProfiles.Single(x => x.Email == Email);
            }
            catch
            {
                user = null;
            }
            return user;

        }

        public void UpdateInformationAboutUser(ClientProfile user)
        {
            ClientProfile clientProfile = Database.ClientProfiles.Single(x => x.Email == user.Email);
            clientProfile.Name = user.Name;
            clientProfile.Email = user.Email;
            clientProfile.Country = user.Country;
            clientProfile.Profilephoto = user.Profilephoto;
            Database.Entry(clientProfile).State = System.Data.Entity.EntityState.Modified;
            Database.SaveChanges();

        }
    }
}
