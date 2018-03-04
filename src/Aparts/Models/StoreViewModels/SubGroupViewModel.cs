using Aparts.Models.DLModels;
using Newtonsoft.Json;

namespace Aparts.Models.StoreViewModels
{
	public class SubGroupViewModel
	{
		public SubGroupViewModel(SubGroup dlSubGroup)
		{
			Id = dlSubGroup.Id;
			IdGroup = dlSubGroup.IdGroup;
			Name = dlSubGroup.Name;
		}

		[JsonProperty("idGroup")]
		public int IdGroup { get; set; }

		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }
	}
}
