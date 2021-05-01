namespace MySportTips.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using MySportTips.Common;
    using MySportTips.Data.Models;
    using MySportTips.Web.ViewModels.Users;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class UsersController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;

        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult ListUsers()
        {
            var users = this.userManager.Users;
            return this.View(users);
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string userId)
        {
            var user = await this.userManager.FindByIdAsync(userId);

            var userModel = new EditUserInputModel()
            {
                UserId = user.Id,
                Email = user.Email,
                UserName = user.UserName,
            };

            var role = this.userManager.GetRolesAsync(user);
            if (role == null)
            {
                userModel.Roles = new List<string>() { "Not in roles" };
            }
            else
            {
                userModel.Roles = role.Result;
            }

            return this.View(userModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserInputModel userInputModel)
        {
            if ( await this.userManager.FindByIdAsync(userInputModel.UserId) == null)
            {
                throw new Exception("This user doesn't exist.");
            }

            if (userInputModel.NewRole != GlobalConstants.AdministratorRoleName &&
                userInputModel.NewRole != GlobalConstants.MemberRoleName &&
                userInputModel.NewRole != GlobalConstants.UserRoleName)
            {
                throw new Exception("The role is invalid");
            }

            if (this.roleManager.FindByNameAsync(userInputModel.NewRole) != null)
            {
                var role = new ApplicationRole(userInputModel.NewRole);
                await this.roleManager.CreateAsync(role);
            }

            var user = await this.userManager.FindByIdAsync(userInputModel.UserId);

            if (userInputModel.OldRole != null)
            {
                await this.userManager.RemoveFromRoleAsync(user, userInputModel.OldRole);
            }

            await this.userManager.AddToRoleAsync(user, userInputModel.NewRole);
            return this.RedirectToAction(nameof(this.ListUsers));
        }
    }
}
