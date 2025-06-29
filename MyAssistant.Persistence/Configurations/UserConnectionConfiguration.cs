using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MyAssistant.Domain.Models;

namespace MyAssistant.Persistence.Configurations
{
    public class UserConnectionConfiguration : IEntityTypeConfiguration<UserConnection>
    {
        public void Configure(EntityTypeBuilder<UserConnection> builder)
        {
            builder.Property(x => x.UserId)
                .IsRequired();

            builder.Property(x => x.FriendUserId)
                .IsRequired();

            builder.Property(x => x.StatusMessage)
                .HasMaxLength(250);

            builder.HasMany(x => x.AuditLogs)
                .WithOne()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasIndex(x => new { x.UserId, x.FriendUserId })
                .IsUnique();
        }
    }
}
