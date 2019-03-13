using BLL.DTO;
using BLL.Interfaces;
using BLL.Services;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialNetworkMvc.Controllers
{
    public class HomeController : Controller
    {


        IServiceCreator serviceCreator = new ServiceCreator();

        private IGroupService CreateGroupService()
        {
            return serviceCreator.CreateGroupService("DefaultConnection");
        }

        public ActionResult Index()
        {
            GroupDTO group = new GroupDTO { Name = "Chotkiy paca", Description = "Best group from all groups" };
           
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}