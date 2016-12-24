using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InternetGameDatabase.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace InternetGameDatabase.Logic
{
    internal class RoleActions
    {
        internal void createAdmin()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            IdentityResult IdRoleResult;
            IdentityResult IdUserResult;
            //set context and assign role management
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleMgr = new RoleManager<IdentityRole>(roleStore);
            //create admin role if it doesn't exist 
            if (!roleMgr.RoleExists("Administrator"))
            {
                IdRoleResult = roleMgr.Create(new IdentityRole("Administrator"));
                if (!IdRoleResult.Succeeded) throw new Exception("Administrator ROLE NOT ADDED!!!");
            }
            if (!roleMgr.RoleExists("User"))
            {
                IdRoleResult = roleMgr.Create(new IdentityRole("User"));
                if (!IdRoleResult.Succeeded) throw new Exception("User ROLE NOT ADDED!!!");
            }

            //Create UserManager as extension of UserStore
            var userStore = new UserStore<ApplicationUser>(context);
            var userMgr = new UserManager<ApplicationUser>(userStore);

            
            //create user and add them to user role
            var regUser = new ApplicationUser()
            {
                UserName = "user@igdb.com",
            };
            userMgr.Create(regUser, "Pa$$word");

            IdUserResult = userMgr.Create(regUser, "Pa$$word");
            if (IdUserResult.Succeeded)
            {
                IdUserResult = userMgr.AddToRole(regUser.Id, "User");
                if (!IdUserResult.Succeeded) throw new Exception("USER NOT ADDED TO USER ROLE");
            }

            //createadmin and add them to administration role
            var appUser = new ApplicationUser()
            {
                UserName = "admin@igdb.com",
            };

            IdUserResult = userMgr.Create(appUser, "Pa$$word");
            if (IdUserResult.Succeeded)
            {
                IdUserResult = userMgr.AddToRole(appUser.Id, "User");
                if (!IdUserResult.Succeeded) throw new Exception("ADMIN USER NOT ADDED TO USER ROLE");
                IdUserResult = userMgr.AddToRole(appUser.Id, "Administrator");
                if (!IdUserResult.Succeeded) throw new Exception("ADMIN USER NOT ADDED TO ADMIN ROLE");
            }         
        }
    }
}