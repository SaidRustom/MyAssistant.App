using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MyAssistant.Domain.Base;

namespace MyAssistant.Persistence.Configurations
{
    public class HistoryEntryConfiguration : IEntityTypeConfiguration<HistoryEntry>
    {
        public void Configure(EntityTypeBuilder<HistoryEntry> builder)
        {
            builder.Property(x => x.PropertyName)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.OldValue)
                .HasMaxLength(4000);

            builder.Property(x => x.NewValue)
                .HasMaxLength(4000);

            builder.Property(x => x.AuditLogId)
                .IsRequired();
        }
    }
}
