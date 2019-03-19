using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using SocialNetworkMvc.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SocialNetworkMvc.Controllers
{
    public class AccountController : Controller
    {

        class Data
        {
            public string name { get; set; }
            public string code { get; set; }
        }

        private IUserService UserService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IUserService>();

            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO { Email = model.Email, Password = model.Password };
                ClaimsIdentity claim = await UserService.Authenticate(userDto);
                if (claim == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");

                }
                else
                {
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    return RedirectToAction("MyPage", "Home");
                }
            }

            return RedirectToAction("Index", "Home");
        }



        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Registration()
        {
           
            string json;

            List<string> items=new List<string>();

            using (WebClient client = new WebClient())
            {
                json = client.DownloadString("https://gist.githubusercontent.com/keeguon/2310008/raw/bdc2ce1c1e3f28f9cab5b4393c7549f38361be4e/countries.json");
            }

            foreach(var i in JsonConvert.DeserializeObject< List < Data >> (json))
            {
                items.Add(i.name.ToString());
            }
           
            SelectList countries = new SelectList(items);

            return View(new RegisterModel { Country=countries});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Registration(RegisterModelPost model)
        {
            await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO
                {
                    Email = model.Email,
                    Password = model.Password,
                    UserName = model.Name,
                    Age = model.Age,
                    Country = model.Country,
                    Name = model.Email,
                    Role = "user"
                };
                /* OperationDetails operationDetails =*/
                await UserService.Create(userDto);
                //if (OperationDetails.Succedeed)
                //    return RedirectToAction("Index", "Home");
                //else
                //    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
                return RedirectToAction("Index", "Home");
            }
            else
                return View();
            
        }
        private async Task SetInitialDataAsync()
        {
            await UserService.SetInitialData(new UserDTO
            {
                Email = "somemail@mail.ru",
                UserName = "somemail@mail.ru",
                Password = "ad46D_ewr3",
                Name = "Семен Семенович Горбунков",
               
                Country = "USA",
                Role = "admin",
            }, new List<string> { "user", "admin" });
        }



        public ActionResult LoadUser()
        {
            var smalluser = UserService.GetUser(User.Identity.Name);
            if(smalluser==null)
            return PartialView("_PartialLoginView", null);
            else
                return PartialView("_PartialLoginView",
                    new UserModel { Name = smalluser.Name});
        }
    }
}