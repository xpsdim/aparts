using System.Collections.Generic;

namespace Aparts.Models.DLModels
{
	public class AspNetRoles
	{
		public string Id { get; set; }

		public string ConcurrencyStamp { get; set; }

		public string Name { get; set; }

		public string NormalizedName { get; set; }

		public virtual ICollection<AspNetRoleClaims> AspNetRoleClaims { get; set; } = new HashSet<AspNetRoleClaims>();

		public virtual ICollection<AspNetUserRoles> AspNetUserRoles { get; set; } = new HashSet<AspNetUserRoles>();
	}
}
