using Aparts.Models.DLModels;
using Newtonsoft.Json;

namespace Aparts.Models.StoreViewModels
{
	public class GroupViewModel
	{
		public GroupViewModel(Group dlGroup)
		{
			Id = dlGroup.Id;
			Name = dlGroup.Name;
		}

		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }
	}
}
