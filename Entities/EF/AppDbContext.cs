using Entities.EF.Configuration;
using Entities.EF.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Entities.EF
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new RolesConfig());

            modelBuilder.Entity<CustomFieldValue>()
                .HasOne<Item>()
                .WithMany(x => x.CustomFieldsValues)
                .HasForeignKey(x => x.ItemId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
