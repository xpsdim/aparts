using System;
using System.Collections.Generic;

using Aparts.Models.DLModels.Documents;

namespace Aparts.Models.DLModels
{
    public class StoreItem
    {
		public int Id { get; set; }

		public int IdSubGroup { get; set; }

		public string CodesByCatalog { get; set; }

		public decimal? Price { get; set; }

		public decimal? PriceIn { get; set; }

		public DateTime? ReplDate { get; set; }

		public virtual SubGroup SubGroup { get; set; }

		public virtual ICollection<CurrentAmount> CurrentAmounts { get; set; } = new HashSet<CurrentAmount>();

		public virtual ICollection<IncomeDocDetail> Incomes { get; set; } = new HashSet<IncomeDocDetail>();
	}
}
