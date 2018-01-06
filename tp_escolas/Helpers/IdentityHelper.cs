using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using tp_escolas.Models;

namespace tp_escolas.Helpers
{
    public class IdentityHelper
    {
        internal static void SeedIdentities(DbContext context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!roleManager.RoleExists(RolesConst.Admin))
            {
                var roleresult = roleManager.Create(new IdentityRole(RolesConst.Admin));
            }
            if (!roleManager.RoleExists(RolesConst.Pai))
            {
                var roleresult = roleManager.Create(new IdentityRole(RolesConst.Pai));
            }
            if (!roleManager.RoleExists(RolesConst.Instituicao))
            {
                var roleresult = roleManager.Create(new IdentityRole(RolesConst.Instituicao));
            }
            string userName = "admina@dmin.com";
            string password = "12345Asd!";
            ApplicationUser user = userManager.FindByName(userName);
            if (user == null)
            {
                user = new ApplicationUser()
                {
                    UserName = userName,
                    Email = userName,
                    EmailConfirmed = true
                };
                IdentityResult userResult = userManager.Create(user, password);
                if (userResult.Succeeded)
                {
                    var result = userManager.AddToRole(user.Id, RolesConst.Admin);
                }
            }
        }

    }
}