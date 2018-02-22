using System;
using System.Collections.Generic;

namespace Aparts.Models.DLModels
{
    public class Group
    {
		public Group()
		{
			Subgroups = new HashSet<SubGroup>();
		}

		public int Id { get; set; }

		public string Name { get; set; }

		public DateTime ReplDate { get; set; }

		public virtual ICollection<SubGroup> Subgroups { get; set; }
	}
}
