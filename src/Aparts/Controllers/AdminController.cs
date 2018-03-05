using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aparts.Models;
using Aparts.Models.AccountViewModels;
using Aparts.Models.AdminViewModelas;
using Aparts.Models.DLModels;
using Aparts.Services;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Aparts.Controllers
{
	[Authorize(Roles = "Admin")]
	public class AdminController : BaseController
	{
		private readonly ManageUserService _manageUserService;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly ApartService _apartService;
		private readonly ImportService _importService;

		public AdminController(ManageUserService manageUserService, UserManager<ApplicationUser> userManager, ApartService apartService, ImportService importService)
		{
			_userManager = userManager;
			_manageUserService = manageUserService;
			_apartService = apartService;
			_importService = importService;
		}

		public IActionResult Index()
		{
			return View(new AdminPageOptions()
			{
				AllowImport = _importService.AllowImport
			});
		}

		public async Task<IActionResult> UserList(DataSourceRequest request)
		{
			var users = _manageUserService.GetUsersForManage();
			//	.Where(u => u.Id != _userManager.GetUserId(User));
			var usersPage = await users.ToDataSourceResultAsync(request);
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
			}

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
			var newRoles = model.Roles ?? new string[] { };
			var currentUserRoles = await _userManager.GetRolesAsync(user);
			
			// deleting unselected roles
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

			// inserting new roles
			if (newRoles.Any())
			{
				await _userManager.AddToRolesAsync(user, newRoles);
			}

			// syncronizing visible stores
			var aspNetUser = _apartService.Context.AspNetUsers.Single(u => u.Id == user.Id);
			var currentUserStores = _apartService.Context.UserVisibleStores
				.Where(vs => vs.UserId == aspNetUser.Id).ToList();
			var newUserStores = model.VisibleStores;
			if (newUserStores == null)
			{
				aspNetUser.UserVisibleStores.Clear();
			}
			else
			{
				foreach (var deletedStore in currentUserStores)
				{
					if (!newUserStores.Select(s => s.Id).Contains(deletedStore.StoreId))
					{
						currentUserStores.Remove(deletedStore);
					}
					else
					{
						newUserStores = newUserStores.Where(s => s.Id != deletedStore.StoreId).ToArray();
					}
				}

				foreach (var newStore in newUserStores)
				{
					_apartService.Context.UserVisibleStores
						.Add(new UserVisibleStores() {StoreId = newStore.Id, UserId = model.Id});
				}
			}

			await _apartService.Context.SaveChangesAsync();
			
			return JsOk();
		}

		public IActionResult Storelist()
		{
			return Json(_apartService
				.GetAllStores()
				.Select(s =>
					new
					{
						id = s.Id,
						caption = s.Caption,
						storeManId = s.Storeman,
						storeManName = s.Storeman != null ? s.StoremanNavigation.Email : string.Empty
					}));
		}

		public IActionResult ImportData()
		{
			if (!_importService.AllowImport)
			{
				return JsError("Import data not allowed!");
			}

			try
			{
				_importService.Import();
			}
			catch (Exception ex)
			{
				return JsError(ex);
			}

			return JsOk();
		}
	}
}