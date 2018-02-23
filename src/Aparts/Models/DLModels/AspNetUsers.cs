using System;
using System.Collections.Generic;

namespace Aparts.Models.DLModels
{
	public class AspNetUsers
	{
		public string Id { get; set; }

		public int AccessFailedCount { get; set; }

		public string ConcurrencyStamp { get; set; }

		public string Email { get; set; }

		public bool EmailConfirmed { get; set; }

		public bool LockoutEnabled { get; set; }

		public DateTimeOffset? LockoutEnd { get; set; }

		public string NormalizedEmail { get; set; }

		public string NormalizedUserName { get; set; }

		public string PasswordHash { get; set; }

		public string PhoneNumber { get; set; }

		public bool PhoneNumberConfirmed { get; set; }

		public string SecurityStamp { get; set; }

		public bool TwoFactorEnabled { get; set; }

		public string UserName { get; set; }

		public byte[] UserPic { get; set; }

		public virtual ICollection<AspNetUserClaims> AspNetUserClaims { get; set; } = new HashSet<AspNetUserClaims>();

		public virtual ICollection<AspNetUserLogins> AspNetUserLogins { get; set; } = new HashSet<AspNetUserLogins>();

		public virtual ICollection<AspNetUserRoles> AspNetUserRoles { get; set; } = new HashSet<AspNetUserRoles>();

		public virtual ICollection<Store> Stores { get; set; } = new HashSet<Store>();

		public virtual ICollection<UserVisibleStores> UserVisibleStores { get; set; } = new HashSet<UserVisibleStores>();
	}
}
