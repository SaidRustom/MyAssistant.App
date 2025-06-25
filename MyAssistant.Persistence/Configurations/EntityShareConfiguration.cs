using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MyAssistant.Domain.Base;

namespace MyAssistant.Persistence.Configurations
{
    public class EntityShareConfiguration : IEntityTypeConfiguration<EntityShare>
    {
        public void Configure(EntityTypeBuilder<EntityShare> builder)
        {
            builder.Property(x => x.EntityType)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.SharedWithUserId)
                .IsRequired();

            builder.Property(x => x.PermissionTypeCode)
                .IsRequired();

            // Use alternate key or composite key for uniqueness
            builder.HasIndex(x => new { x.EntityId, x.EntityType, x.SharedWithUserId })
                .IsUnique();
        }
    }
}
