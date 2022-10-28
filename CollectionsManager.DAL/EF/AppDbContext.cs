using CollectionsManager.DAL.EF.Configuration;
using CollectionsManager.DAL.Entities;
using CollectionsManager.DAL.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CollectionsManager.DAL.EF
{
    public class AppDbContext : IdentityDbContext<User, Role, string, IdentityUserClaim<string>, UserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Collection> Collections { get; set; }

        public DbSet<Item> Items { get; set; }

        public DbSet<CustomField> CustomFields { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Like> Likes { get; set; }

        public DbSet<CustomFieldValue> CustomFieldValues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(x => new { x.UserId, x.RoleId });

                userRole.HasOne(x => x.Role)
                    .WithMany(x => x.UserRoles)
                    .HasForeignKey(x => x.RoleId)
                    .IsRequired();

                userRole.HasOne(x => x.User)
                    .WithMany(x => x.UserRoles)
                    .HasForeignKey(x => x.UserId)
                    .IsRequired();
            });

            modelBuilder
                .Entity<Collection>()
                .Property(x => x.ImageSource)
                .HasDefaultValue("https://storage.googleapis.com/download/storage/v1/b/collections-images-bucket/o/638025681499527187.jpg?generation=1666960550830642&alt=media");

            modelBuilder.ApplyConfiguration(new RolesConfig());
        }
    }
}
