using System;

using Aparts.Models.MessageModels; 
using Microsoft.AspNetCore.Mvc;

namespace Aparts.Controllers
{
    public class BaseController	: Controller
	{
	    protected JsonResponse JsonOkResult()
	    {
		    return new JsonResponse();
	    }

		protected JsonResponse JsErrorResult(string errorMessage)
	    {
		    return new JsonResponse(errorMessage);
	    }

		protected JsonResult JsOk()
	    {
		    return Json(JsonOkResult());
	    }

		protected JsonResult JsError(string errorMessage)
		{
			return Json(JsErrorResult(errorMessage));
		}

		protected JsonResult JsError(Exception ex)
		{
			return JsError(ex.Message);
		}
	}
}
