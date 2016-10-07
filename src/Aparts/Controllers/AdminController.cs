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
        private readonly ApartService _apartService;

        public AdminController(ManageUserService manageUserService, UserManager<ApplicationUser> userManager, ApartService apartService)
        {
            _userManager = userManager;
            _manageUserService = manageUserService;
            _apartService = apartService;
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
                    Roles = (await _userManager.GetRolesAsync(user)).ToArray(),
                    VisibleStores = _apartService.GetVisibleStores(user.Id)
                });
            };
                                    
            usersPage.Data = model;
            return Json(usersPage);
        }

        public IActionResult RolesList()
        {
            return Json(_manageUserService.GetAllRoles());
        }
        
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateUser(UserRolesModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            var newRoles = model.Roles ?? new string[] {};
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
            //syncronizing visible stores
            var currentUserStores = _apartService.Context.AspNetUsers.Single(u => u.Id == user.Id).UserVisibleStores.ToList();
            //currentUserStores.RemoveAll(true);
            

            return Json(new {result = "ok"});
        }

        public IActionResult Storelist()
        {
            return Json(_apartService
                .GetAllStores().
                Select(s => 
                    new
                    {
                        id = s.Id,
                        caption = s.Caption,
                        storeManId = s.Storeman,
                        storeManName = s.Storeman != null ? s.StoremanNavigation.Email : ""
                    }));
        }
    }
}