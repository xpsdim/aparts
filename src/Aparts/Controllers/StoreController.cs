using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aparts.Models.StoreViewModels;
using Aparts.Services;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;

namespace Aparts.Controllers
{
	public class StoreController : Controller
	{
		private readonly ApartService _apartService;

		public StoreController(ApartService apartService)
		{
			_apartService = apartService;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Manage()
		{
			return View();
		}

		public IActionResult GroupList()
		{
			var groups = _apartService.Context.Groups
				.Select(g => new GroupViewModel(g));
			return Json(groups.ToDataSourceResult(new DataSourceRequest()));
		}
	}
}