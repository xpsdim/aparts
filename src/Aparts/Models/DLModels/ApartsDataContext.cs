using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Aparts.Models.DLModels
{
	public class ApartsDataContext : DbContext
	{
		public ApartsDataContext(DbContextOptions<ApartsDataContext> options)
			: base(options)
		{
		}

		public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }

		public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }

		public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }

		public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }

		public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }

		public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }

		public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }

		public virtual DbSet<Store> Stores { get; set; }

		public virtual DbSet<UserVisibleStores> UserVisibleStores { get; set; }

		public virtual DbSet<Group> Groups { get; set; }

		public virtual DbSet<SubGroup> SubGroups { get; set; }

		public virtual DbSet<StoreItem> StoreItems { get; set; }

		public virtual DbSet<CurrentAmount> CurrentAmounts { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<AspNetRoleClaims>(entity =>
			{
				entity.HasIndex(e => e.RoleId)
					.HasName("IX_AspNetRoleClaims_RoleId");

				entity.Property(e => e.RoleId)
					.IsRequired()
					.HasMaxLength(450);

				entity.HasOne(d => d.Role)
					.WithMany(p => p.AspNetRoleClaims)
					.HasForeignKey(d => d.RoleId);
			});

			modelBuilder.Entity<AspNetRoles>(entity =>
			{
				entity.HasIndex(e => e.NormalizedName)
					.HasName("RoleNameIndex");

				entity.Property(e => e.Id).HasMaxLength(450);

				entity.Property(e => e.Name).HasMaxLength(256);

				entity.Property(e => e.NormalizedName).HasMaxLength(256);
			});

			modelBuilder.Entity<AspNetUserClaims>(entity =>
			{
				entity.HasIndex(e => e.UserId)
					.HasName("IX_AspNetUserClaims_UserId");

				entity.Property(e => e.UserId)
					.IsRequired()
					.HasMaxLength(450);

				entity.HasOne(d => d.User)
					.WithMany(p => p.AspNetUserClaims)
					.HasForeignKey(d => d.UserId);
			});

			modelBuilder.Entity<AspNetUserLogins>(entity =>
			{
				entity.HasKey(e => new { e.LoginProvider, e.ProviderKey })
					.HasName("PK_AspNetUserLogins");

				entity.HasIndex(e => e.UserId)
					.HasName("IX_AspNetUserLogins_UserId");

				entity.Property(e => e.LoginProvider).HasMaxLength(450);

				entity.Property(e => e.ProviderKey).HasMaxLength(450);

				entity.Property(e => e.UserId)
					.IsRequired()
					.HasMaxLength(450);

				entity.HasOne(d => d.User)
					.WithMany(p => p.AspNetUserLogins)
					.HasForeignKey(d => d.UserId);
			});

			modelBuilder.Entity<AspNetUserRoles>(entity =>
			{
				entity.HasKey(e => new { e.UserId, e.RoleId })
					.HasName("PK_AspNetUserRoles");

				entity.HasIndex(e => e.RoleId)
					.HasName("IX_AspNetUserRoles_RoleId");

				entity.HasIndex(e => e.UserId)
					.HasName("IX_AspNetUserRoles_UserId");

				entity.Property(e => e.UserId).HasMaxLength(450);

				entity.Property(e => e.RoleId).HasMaxLength(450);

				entity.HasOne(d => d.Role)
					.WithMany(p => p.AspNetUserRoles)
					.HasForeignKey(d => d.RoleId);

				entity.HasOne(d => d.User)
					.WithMany(p => p.AspNetUserRoles)
					.HasForeignKey(d => d.UserId);
			});

			modelBuilder.Entity<AspNetUserTokens>(entity =>
			{
				entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name })
					.HasName("PK_AspNetUserTokens");

				entity.Property(e => e.UserId).HasMaxLength(450);

				entity.Property(e => e.LoginProvider).HasMaxLength(450);

				entity.Property(e => e.Name).HasMaxLength(450);
			});

			modelBuilder.Entity<AspNetUsers>(entity =>
			{
				entity.HasIndex(e => e.NormalizedEmail)
					.HasName("EmailIndex");

				entity.HasIndex(e => e.NormalizedUserName)
					.HasName("UserNameIndex")
					.IsUnique();

				entity.Property(e => e.Id).HasMaxLength(450);

				entity.Property(e => e.Email).HasMaxLength(256);

				entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

				entity.Property(e => e.NormalizedUserName)
					.IsRequired()
					.HasMaxLength(256);

				entity.Property(e => e.UserName).HasMaxLength(256);
			});

			modelBuilder.Entity<Store>(entity =>
			{
				entity.Property(e => e.Id).ValueGeneratedNever();

				entity.Property(e => e.Caption)
					.IsRequired()
					.HasMaxLength(16);

				entity.Property(e => e.Storeman).HasMaxLength(450);

				entity.HasOne(d => d.StoremanNavigation)
					.WithMany(p => p.Stores)
					.HasForeignKey(d => d.Storeman)
					.OnDelete(DeleteBehavior.SetNull)
					.HasConstraintName("FK_Storeman_UserId");
			});

			modelBuilder.Entity<UserVisibleStores>(entity =>
			{
				entity.HasKey(e => new { e.UserId, e.StoreId })
					.HasName("PK__UserVisi__1430E35C337D149B");

				entity.Property(e => e.UserId).HasMaxLength(450);

				entity.HasOne(d => d.Store)
					.WithMany(p => p.UserVisibleStores)
					.HasForeignKey(d => d.StoreId)
					.HasConstraintName("FK_VisibleStore_StoreId");

				entity.HasOne(d => d.User)
					.WithMany(p => p.UserVisibleStores)
					.HasForeignKey(d => d.UserId)
					.HasConstraintName("FK_VisibleStore_UserId");
			});

			modelBuilder.Entity<SubGroup>(
				entity =>
					{
						entity.HasOne(e => e.Group).WithMany(p => p.Subgroups).HasForeignKey(d => d.IdGroup);
					});

			modelBuilder.Entity<StoreItem>(
				entity =>
				{
					entity.HasOne(e => e.SubGroup).WithMany(p => p.StoreItems).HasForeignKey(d => d.IdSubGroup);
				});

			modelBuilder.Entity<CurrentAmount>(
				entity =>
					{
						entity.HasOne(e => e.Store).WithMany(p => p.StoreItemsAmounts).HasForeignKey(d => d.IdStore);
						entity.HasOne(e => e.StoreItem).WithMany(p => p.CurrentAmounts).HasForeignKey(d => d.StoreItem);
					});
		}
	}
}