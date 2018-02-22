using System;
using System.Collections.Generic;

namespace Aparts.Models.DLModels
{
    public class SubGroup
    {
		public int Id { get; set; }

		public int IdGroup { get; set; }

		public string Name { get; set; }

		public DateTime? ReplDate { get; set; }

		public virtual Group Group { get; set; }

		public virtual ICollection<StoreItem> StoreItems { get; set; } = new HashSet<StoreItem>();
	}
}
