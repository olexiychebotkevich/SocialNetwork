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


        private IGroupService GroupService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IGroupService>();

            }
        }



        private IPostService PostService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IPostService>();

            }
        }



        private IUserService UserService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IUserService>();

            }
        }



        [HttpGet]
        public ActionResult Index()
        {

            var user = UserService.GetUser(User.Identity.Name);
            if (user != null)
                return RedirectToAction("MyPage", "Home");
            return View();
        }

        public ActionResult Friends()
        {
           

            return View();
        }

        public ActionResult People()
        {
           

            return View();
        }
        [HttpGet]
        public ActionResult MyPage()
        {
            

                return View();
        }
        public ActionResult Groups()
        {


            return View();
        }
        public ActionResult MyGroups()
        {


            return View();
        }
        public ActionResult Group()
        {


            return View();
        }


        [HttpGet]
        public  ActionResult AddGroup()
        {
            GroupDTO group = new GroupDTO { Name = $"New Group {DateTime.Now.Second} ", Description = "Best group from all groups in world" };

            GroupService.Create(group);

            ViewBag.Message = "Group Added";

            return View();
        }



        [HttpGet]
        public ActionResult AddPost()
        {
            PostDTO postDTO = new PostDTO { Author = "Vasyan", Subject = "Fun", Date = DateTime.Now, Text = "New Vasysan post" };

            PostService.Create(postDTO);

            ViewBag.Message = "Post Added";

            return View();
        }
    }
}