﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MyAssistant.Domain.Base;

namespace MyAssistant.Persistence.Configurations
{
    public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            builder.Property(x => x.EntityType)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.UserId)
                .IsRequired();

            builder.Property(x => x.ActionTypeCode)
                .IsRequired();

            builder.HasMany(x => x.HistoryEntries)
                .WithOne()
                .HasForeignKey(x => x.AuditLogId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
