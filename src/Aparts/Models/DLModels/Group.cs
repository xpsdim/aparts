using System;
using System.Collections.Generic;

namespace Aparts.Models.DLModels
{
	public class Group
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public DateTime? ReplDate { get; set; }

		public virtual ICollection<SubGroup> Subgroups { get; set; } = new HashSet<SubGroup>();
	}
}
