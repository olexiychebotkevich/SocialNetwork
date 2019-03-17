﻿using BLL.DTO;
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
                return RedirectToAction("MyPage", "Home");
            return View();
        }

        public ActionResult Friends()
        {
           

            return View();
        }

        public ActionResult People()
        {

            List<UserDTO> userDTOs = UserService.GetAllUsers();
            List<UserModel> userModels=null;
            foreach(var i in userDTOs)
            {
                userModels.Add(new UserModel
                {
                    Name = i.Name,
                   
                    Country = i.Country
                });
            }
            return View(userModels);
        }


        [HttpGet]
        public ActionResult MyPage()
        {

            UserDTO user = UserService.GetUser(User.Identity.Name);
            MyPageModel myPageModel = new MyPageModel
            {
                Name = user.Name,
                Email = user.Email,
                Age=user.Age,
                Country=user.Country,
                ProfilePhoto=user.Profilephoto
                
            };
                return View(myPageModel);
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
        public  ActionResult AddGroup_()
        {
            GroupDTO group = new GroupDTO { Name = $"New Group {DateTime.Now.Second} ", Description = "Best group from all groups in world" };

            GroupService.Create(group);

            ViewBag.Message = "Group Added";

            return View();
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
    }
}