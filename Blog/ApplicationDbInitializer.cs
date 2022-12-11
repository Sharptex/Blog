using Blog_DAL.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Blog
{
    public static class ApplicationDbInitializer
    {
        public static void SeedUsers(UserManager<User> userManager)
        {
            Role roleUser = null;

            if (userManager.FindByNameAsync("Admin2").Result == null)
            {
                User user = new User { UserName = "Admin2" };
                var result = userManager.CreateAsync(user, "AdminPass").Result;

                if (result.Succeeded)
                {
                    roleUser = new Role { Name = "DefaultUser" };
                    var roleAdmin = new Role { Name = "Admin" };
                    user.Roles = new List<Role>() { roleUser, roleAdmin };
                    userManager.UpdateAsync(user).Wait();

                    var claim1 = new Claim("Role", roleUser.Name);
                    var claim2 = new Claim("Role", roleAdmin.Name);
                    userManager.AddClaimAsync(user, claim1).Wait();
                    userManager.AddClaimAsync(user, claim2).Wait();
                }
            }

            if (userManager.FindByNameAsync("Moderator").Result == null)
            {
                User user = new User { UserName = "Moderator" };
                var result = userManager.CreateAsync(user, "ModeratorPass").Result;

                if (result.Succeeded)
                {
                    var roleModerator = new Role { Name = "Moderator" };
                    user.Roles = new List<Role>() { roleUser, roleModerator };
                    userManager.UpdateAsync(user).Wait();

                    var claim1 = new Claim("Role", roleUser.Name);
                    var claim2 = new Claim("Role", roleModerator.Name);
                    userManager.AddClaimAsync(user, claim1).Wait();
                    userManager.AddClaimAsync(user, claim2).Wait();
                }
            }

            if (userManager.FindByNameAsync("SimpleUser").Result == null)
            {
                User user = new User { UserName = "SimpleUser" };
                var result = userManager.CreateAsync(user, "SimpleUserPass").Result;

                if (result.Succeeded)
                {
                    user.Roles = new List<Role>() { roleUser };
                    userManager.UpdateAsync(user).Wait();

                    var claim1 = new Claim("Role", roleUser.Name);
                    userManager.AddClaimAsync(user, claim1).Wait();
                }
            }
        }
    }
}