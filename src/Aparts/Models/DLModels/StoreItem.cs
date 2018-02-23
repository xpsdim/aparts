using System;
using System.Collections.Generic;

namespace Aparts.Models.DLModels
{
    public class StoreItem
    {
		public int Id { get; set; }

		public int IdSubGroup { get; set; }

		public string CodesByCatalog { get; set; }

		public decimal? Price { get; set; }

		public DateTime? ReplDate { get; set; }

		public virtual SubGroup SubGroup { get; set; }

		public virtual ICollection<CurrentAmount> CurrentAmounts { get; set; }
	}
}
