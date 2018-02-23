﻿using System.Collections.Generic;

namespace Aparts.Models.DLModels
{
	public class Store
	{
		public int Id { get; set; }

		public string Caption { get; set; }

		public string Storeman { get; set; }

		public virtual ICollection<UserVisibleStores> UserVisibleStores { get; set; } = new HashSet<UserVisibleStores>();

		public virtual AspNetUsers StoremanNavigation { get; set; }

		public virtual ICollection<CurrentAmount> StoreItemsAmounts { get; set; }
	}
}