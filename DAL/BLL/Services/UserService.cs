using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork Database { get; set; }

        public UserService(IUnitOfWork uow)
        {
            Database = uow;
        }


        

        public async Task<OperationDetails> Create(UserDTO userDto)
        {
            ApplicationUser user = await Database.UserManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new ApplicationUser { Email = userDto.Email, UserName = userDto.Email};
                var result = await Database.UserManager.CreateAsync(user, userDto.Password);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                // добавляем роль
                await Database.UserManager.AddToRoleAsync(user.Id, userDto.Role);
                // создаем профиль клиента
                ClientProfile clientProfile = new ClientProfile { Id = user.Id,  Name = userDto.UserName,Age=userDto.Age,Country=userDto.Country,Email=userDto.Email };
                Database.ClientManager.Create(clientProfile);
                await Database.SaveAsync();
                return new OperationDetails(true, "Регистрация успешно пройдена", "");
            }
            else
            {
                return new OperationDetails(false, "Пользователь с таким логином уже существует", "Email");
            }
        }

        public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
        {
            ClaimsIdentity claim = null;
            // находим пользователя
            userDto.Name = "Vasyan";
            ApplicationUser user = await Database.UserManager.FindAsync(userDto.Email, userDto.Password);
            
            
            // авторизуем его и возвращаем объект ClaimsIdentity
            if (user != null)
                claim = await Database.UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }

        // начальная инициализация бд
        public async Task SetInitialData(UserDTO adminDto, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await Database.RoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new ApplicationRole { Name = roleName };
                    await Database.RoleManager.CreateAsync(role);
                }
            }
            await Create(adminDto);
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public UserDTO GetUser(string Email)
        {
            UserDTO UserDTO = null;
            ClientProfile user = Database.ClientManager.GetUserByEmail(Email);
            if(user!=null)
            UserDTO = new UserDTO { Name = user.Name, Email = user.Email,Country=user.Country,Age=user.Age,UserName=user.Name,Profilephoto=user.Profilephoto};
            return UserDTO;
        }

        public List<UserDTO> GetAllUsers()
        {
           
            List<UserDTO> userDTOs=new List<UserDTO>();
           
           

            foreach(var i in Database.ClientManager.GetUsers())
            {

                userDTOs.Add(
                    new UserDTO
                    {
                        Name = i.Name,
                        Age = i.Age,
                        Profilephoto=i.Profilephoto,
                        Country = i.Country,
                        Email = i.Email
                        
                        
                    });
            }

            return userDTOs;
            
        }

        public void UpdateInformation(UserDTO user)
        {
            Database.ClientManager.UpdateInformationAboutUser(new ClientProfile { Name = user.Name, Age = user.Age, Country = user.Country, Profilephoto = user.Profilephoto, Email = user.Email });
        }
    }
}
