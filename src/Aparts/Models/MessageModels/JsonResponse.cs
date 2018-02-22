using Newtonsoft.Json;

namespace Aparts.Models.MessageModels
{
    public class JsonResponse
    {
	    public JsonResponse()
	    {
		    Success = true;
	    }

		public JsonResponse(string errorStatus)
		{
			Success = false;
			Status = errorStatus;
		}

		[JsonProperty("success")]
		public bool Success { get; set; }

		[JsonProperty("status")]
		public string Status { get; set; }
	}
}
