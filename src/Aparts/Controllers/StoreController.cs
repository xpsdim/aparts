using System.Linq;
using System.Threading.Tasks;
using Aparts.Models;
using Aparts.Models.DLModels;
using Aparts.Models.StoreViewModels;
using Aparts.Services;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Aparts.Controllers
{
	public class StoreController : Controller
	{
		private readonly ApartService _apartService;
		private readonly UserManager<ApplicationUser> _userManager;

		public StoreController(ApartService apartService, UserManager<ApplicationUser> userManager)
		{
			_apartService = apartService;
			_userManager = userManager;
		}

		public IActionResult Index()
		{
            var model = new StorePageOptions()
            {
                VisibleStores = _apartService.GetVisibleStores(_userManager.GetUserId(User))
            };

            return View(model);
		}

		public IActionResult Manage()
		{
			return View();
		}

		public async Task<IActionResult> GroupList()
		{
			var groups = _apartService.Context.Groups
				.Select(g => new GroupViewModel(g));
			return Json(await groups.ToDataSourceResultAsync(new DataSourceRequest()));
		}

		public async Task<IActionResult> Subgroups([DataSourceRequest] DataSourceRequest request)
		{
			var subGroups = await _apartService.Context.SubGroups
				.Select(sub => new SubGroupViewModel(sub))
				.ToDataSourceResultAsync(request);
			return Json(subGroups);
		}

		public async Task<IActionResult> StoreItems([DataSourceRequest] DataSourceRequest request)
		{
            var ammoumtList = _apartService.GetCurrenAmountsVisibleToUser(_userManager.GetUserId(User));

			var storeItems = await _apartService.Context.StoreItems
                .Select(item => new StoreItemViewModel(item))
				.ToDataSourceResultAsync(request);

            foreach(StoreItemViewModel item in storeItems.Data)
            {
                var amounts = ammoumtList.Where(a => a.IdStoreItem == item.Id).ToArray();
                item.SetAmounts(amounts);

            }

			return Json(storeItems);
		}
	}
}