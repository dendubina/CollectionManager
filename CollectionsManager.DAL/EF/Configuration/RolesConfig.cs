using CollectionsManager.DAL.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollectionsManager.DAL.EF.Configuration
{
    internal class RolesConfig : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(DataForInitialize.Roles);
        }
    }
}
