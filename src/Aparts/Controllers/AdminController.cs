using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aparts.Models;
using Aparts.Models.AccountViewModels;
using Aparts.Services;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace Aparts.Controllers
{

    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ManageUserService _manageUserService;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(ManageUserService manageUserService, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _manageUserService = manageUserService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> UserList(DataSourceRequest request)
        {
            var users = _manageUserService.GetUsersForManage()
                .Where(u => u.Id != _userManager.GetUserId(User));
            var usersPage = users.ToDataSourceResult(request);
            var model = new List<UserRolesModel>();
            foreach (ApplicationUser user in usersPage.Data)
            {
                model.Add(new UserRolesModel()
                {
                    Email = user.Email,
                    Id = user.Id,
                    Roles = (await _userManager.GetRolesAsync(user)).ToArray()
                });
            }
            usersPage.Data = model;
            return Json(usersPage);
        }

        public IActionResult RolesList()
        {
            return Json(_manageUserService.GetAllRoles());
        }
        
        public async Task<IActionResult> UpdateRoles(UserRolesModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            var newRoles = model.Roles;
            var currentUserRoles = await _userManager.GetRolesAsync(user);
            //deleting unselected roles
            foreach (var role in currentUserRoles)
            {
                if (!newRoles.Contains(role))
                {
                    await _userManager.RemoveFromRoleAsync(user, role);
                }
                else
                {
                    newRoles = newRoles.Where(r => r != role).ToArray();
                }
            }
            //inserting new roles
            if (newRoles.Any())
            {
                await _userManager.AddToRolesAsync(user, newRoles);
            }
            return Json(new {result = "ok"});
        }
    }
}