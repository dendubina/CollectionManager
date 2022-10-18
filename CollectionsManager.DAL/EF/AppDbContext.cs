using CollectionsManager.DAL.EF.Configuration;
using CollectionsManager.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CollectionsManager.DAL.EF
{
    public class AppDbContext : IdentityDbContext<User>
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

            modelBuilder
                .Entity<Collection>()
                .Property(x => x.ImageSource)
                .HasDefaultValue("https://embdesignshop.com/frontassets/images/no_image.jpg");

            modelBuilder.ApplyConfiguration(new RolesConfig());
        }
    }
}
