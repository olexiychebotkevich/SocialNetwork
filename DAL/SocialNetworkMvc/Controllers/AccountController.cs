using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SocialNetworkMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SocialNetworkMvc.Controllers
{
    public class AccountController : Controller
    {
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

        public ActionResult LoginModel()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LoginModel(LoginModel model)
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
            return View(new RegisterModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Registration(RegisterModel model)
        {
            await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO
                {
                    Email = model.Email,
                    Password = model.Password,
                    UserName = model.Name,
                    Age=model.Age,
                    City=model.City,
                    Country=model.Country,
                    Name=model.Name,
                    Role = "user"
                };
                /* OperationDetails operationDetails =*/
                await UserService.Create(userDto);
                //if (operationDetails.Succedeed)
                //    return RedirectToAction("Index","Home");
                //else
                //    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return RedirectToAction("Index","Home");
        }
        private async Task SetInitialDataAsync()
        {
            await UserService.SetInitialData(new UserDTO
            {
                Email = "somemail@mail.ru",
                UserName = "somemail@mail.ru",
                Password = "ad46D_ewr3",
                Name = "Семен Семенович Горбунков",
                City = "New-Yourk",
                Country = "New-Yourk",
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