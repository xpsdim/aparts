namespace Aparts.Models.DLModels.Documents
{
    public class IncomeDocDetail
    {
		public int Id { get; set; }
		
		public int IdHeader { get; set; }

		public int IdStoreItem { get; set; }

		public decimal? Amount { get; set; }

		public decimal? PriceIn { get; set; }

		public decimal? PriceOut { get; set; }

		public virtual StoreItem StoreItem { get; set; }

		public virtual IncomeDoc IncomeDoc { get; set; }
	}
}
