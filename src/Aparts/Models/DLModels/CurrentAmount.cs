namespace Aparts.Models.DLModels
{
    public class CurrentAmount
    {
		public int IdStoreItem { get; set; }

		public int IdStore { get; set; }

		public decimal Amount { get; set; }

		public virtual Store Store { get; set; }

		public virtual StoreItem StoreItem { get; set; }
	}
}
