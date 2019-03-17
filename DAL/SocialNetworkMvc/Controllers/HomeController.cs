using BLL.DTO;
using BLL.Interfaces;
using BLL.Services;
using Microsoft.AspNet.Identity.Owin;
using SocialNetworkMvc.Models;
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
            {
               
                return RedirectToAction("MyPage", "Home");
            }
                
            return View();
        }

        public ActionResult Friends()
        {
           

            return View();
        }

        public ActionResult People()
        {

            List<UserDTO> userDTOs = UserService.GetAllUsers();
            List<UserModel> userModels=new List<UserModel>();
            foreach(var i in userDTOs)
            {
                userModels.Add(new UserModel
                {
                    Name = i.Name,
                    Country = i.Country,
                    Age=i.Age,
                    Email=i.Email,
                    ProfilePhoto=i.Profilephoto

                    
                });
            }
            return View(userModels);
        }


        [HttpGet]
        public ActionResult MyPage()
        {

            UserDTO user = UserService.GetUser(User.Identity.Name);
            if (user != null)
            {
                MyPageModel myPageModel = new MyPageModel
                {
                    Name = user.Name,
                    Email = user.Email,
                    Age = user.Age,
                    Country = user.Country,
                    ProfilePhoto = user.Profilephoto

                };
                return View(myPageModel);
            }
            else
                return RedirectToAction("Index", "Home");
        }
        public ActionResult Groups()
        {
            List<GroupsModel> groups = new List<GroupsModel>();
            foreach (var g in GroupService.GetGroups())
            {
                groups.Add(new GroupsModel { Name = g.Name, Description = g.Description, MainImage = g.MainImage });
            }
           
            return View(groups);
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
        public  ActionResult AddGroup_()
        {
            return View();
        }


        [HttpPost]
        public ActionResult AddGroup_(AddGroupModel group)
        {
            GroupService.Create(new GroupDTO { Name = group.Name, Description = group.Description, MainImage = group.MainImage });
            return RedirectToAction("MyPage");
        }













        [HttpGet]
        public ActionResult AddPost_()
        {
            PostDTO postDTO = new PostDTO { Author = "Vasyan", Subject = "Fun", Date = DateTime.Now, Text = "New Vasysan post" };

            PostService.Create(postDTO);

            ViewBag.Message = "Post Added";

            return View();
        }



        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase upload)
        {
            if (upload != null)
            {
                UserDTO user = UserService.GetUser(User.Identity.Name); ;
                // получаем имя файла
                string fileName = System.IO.Path.GetFileName(upload.FileName);
                // сохраняем файл в папку Files в проекте
                upload.SaveAs(Server.MapPath($"/UsersPhotos/" + user.UserName + fileName ));

                user.Profilephoto = user.UserName + fileName;
                UserService.UpdateInformation(user);
                
            }
            return RedirectToAction("MyPage");
        }


        [HttpPost]
        public ActionResult UploadGroupPhoto(HttpPostedFileBase upload)
        {
            AddGroupModel addgroup=new AddGroupModel();
            if (upload != null)
            {
                addgroup = new AddGroupModel();

                // получаем имя файла
                string fileName = System.IO.Path.GetFileName(upload.FileName);
                // сохраняем файл в папку Files в проекте
                upload.SaveAs(Server.MapPath($"/GroupsPhotos/" +DateTime.Now.ToString("dd.MM.yyyy") + User.Identity.Name+ fileName));

                addgroup.MainImage = DateTime.Now.ToString("dd.MM.yyyy") + User.Identity.Name + fileName;
               

            }
            return View("AddGroup_", addgroup);
        }
    }
}