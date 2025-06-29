using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MyAssistant.Domain.Models;

namespace MyAssistant.Persistence.Configurations
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Message)
                .HasMaxLength(200);

            builder.Property(x => x.ActionUrl)
                .HasMaxLength(400);

            builder.HasMany(x => x.AuditLogs)
                .WithOne()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
