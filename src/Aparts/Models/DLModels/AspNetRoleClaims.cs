namespace Aparts.Models.DLModels
{
	public class AspNetRoleClaims
	{
		public int Id { get; set; }

		public string ClaimType { get; set; }

		public string ClaimValue { get; set; }

		public string RoleId { get; set; }

		public virtual AspNetRoles Role { get; set; }
	}
}
