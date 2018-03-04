using System.Linq;
using Aparts.Models.ApartModels;
using Aparts.Models.DLModels;
using Newtonsoft.Json;

namespace Aparts.Models.StoreViewModels
{
	public class StoreItemViewModel
	{
		public StoreItemViewModel(StoreItem storeItem, StoreViewModel[] visibleStores)
		{
			Id = storeItem.Id;
			IdSubGroup = storeItem.IdSubGroup;
			CodesByCatalog = storeItem.CodesByCatalog;
			Price = storeItem.Price;
			PriceIn = storeItem.PriceIn;
			var visibleAmounts = storeItem.CurrentAmounts
				.Where(a => visibleStores.Select(s => s.Id).Contains(a.Store.Id))
				.ToArray();
			foreach (var amount in visibleAmounts)
			{
				switch (amount.Store.Id)
				{
					case 0:
						T1 = amount.Amount;
						break;
					case 1:
						T2 = amount.Amount;
						break;
					case 2:
						T3 = amount.Amount;
						break;
					case 3:
						T4 = amount.Amount;
						break;
					case 4:
						T5 = amount.Amount;
						break;
					case 5:
						T6 = amount.Amount;
						break;
					case 6:
						T7 = amount.Amount;
						break;
					case 7:
						T8 = amount.Amount;
						break;
					case 8:
						T9 = amount.Amount;
						break;
					case 9:
						T10 = amount.Amount;
						break;
				}
			}
		}
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("idSubGroup")]
		public int IdSubGroup { get; set; }

		[JsonProperty("codesByCatalog")]
		public string CodesByCatalog { get; set; }

		[JsonProperty("price")]
		public decimal? Price { get; set; }

		[JsonProperty("priceIn")]
		public decimal? PriceIn { get; set; }

		[JsonProperty("t1")]
		public decimal? T1 { get; set; }

		[JsonProperty("t2")]
		public decimal? T2 { get; set; }

		[JsonProperty("t3")]
		public decimal? T3 { get; set; }

		[JsonProperty("t4")]
		public decimal? T4 { get; set; }

		[JsonProperty("t5")]
		public decimal? T5 { get; set; }

		[JsonProperty("t6")]
		public decimal? T6 { get; set; }

		[JsonProperty("t7")]
		public decimal? T7 { get; set; }

		[JsonProperty("t8")]
		public decimal? T8 { get; set; }

		[JsonProperty("t9")]
		public decimal? T9 { get; set; }

		[JsonProperty("t10")]
		public decimal? T10 { get; set; }
	}
}
