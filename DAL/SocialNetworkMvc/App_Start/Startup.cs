﻿using BLL.Interfaces;
using BLL.Services;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


[assembly: OwinStartup(typeof(SocialNetworkMvc.App_Start.Startup))]
namespace SocialNetworkMvc.App_Start
{
    public class Startup
    {
        IServiceCreator serviceCreator = new ServiceCreator();
        public void Configuration(IAppBuilder app)
        {
            
            app.CreatePerOwinContext<IUserService>(CreateUserService);

            app.CreatePerOwinContext<IGroupService>(CreateGroupService);


            app.CreatePerOwinContext<IPostService>(CreatePostService);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }

        private IUserService CreateUserService()
        {
            return serviceCreator.CreateUserService("DefaultConnection");
        }


        private IGroupService CreateGroupService()
        {
            return serviceCreator.CreateGroupService("DefaultConnection");
        }


        private IPostService CreatePostService()
        {
            return serviceCreator.CreatePostService("DefaultConnection");
        }


    }
}