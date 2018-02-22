namespace Aparts.Models.DLModels
{
	public class UserVisibleStores
	{
		public string UserId { get; set; }

		public int StoreId { get; set; }

		public virtual Stores Store { get; set; }

		public virtual AspNetUsers User { get; set; }
	}
}
