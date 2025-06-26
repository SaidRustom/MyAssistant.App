using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MyAssistant.Domain.Models;

namespace MyAssistant.Persistence.Configurations
{
    public class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem>
    {
        public void Configure(EntityTypeBuilder<TaskItem> builder)
        {
            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Description)
                .HasMaxLength(1000);

            builder.Property(x => x.RecurrenceTypeCode)
                .HasMaxLength(100);

            builder.HasOne(x => x.LinkedGoal)
                .WithMany(g => g.LinkedTasks)
                .HasForeignKey(x => x.GoalId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(x => x.Shares)
                .WithOne()
                .HasForeignKey(s => new { s.EntityId, s.EntityType })
                .HasPrincipalKey(t => new { EntityId = t.Id, EntityType = nameof(TaskItem) })
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.AuditLogs)
                .WithOne()
                .HasForeignKey(a => a.EntityId)
                .HasPrincipalKey(t => t.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
