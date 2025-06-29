using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MyAssistant.Domain.Models;

namespace MyAssistant.Persistence.Configurations
{
    public class ChatMessageConfiguration : IEntityTypeConfiguration<ChatMessage>
    {
        public void Configure(EntityTypeBuilder<ChatMessage> builder)
        {
            builder.Property(x => x.ReceiverUserId)
                .IsRequired();

            builder.Property(x => x.Content)
                .IsRequired();

            builder.Property(x => x.AttachmentUrl)
                .HasMaxLength(200);

            builder.Property(x => x.MessageType)
                .HasMaxLength(100);

            builder.HasMany(x => x.AuditLogs)
                .WithOne()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasIndex(x => new { x.UserId, x.ReceiverUserId, x.SentAt });
        }
    }
}
