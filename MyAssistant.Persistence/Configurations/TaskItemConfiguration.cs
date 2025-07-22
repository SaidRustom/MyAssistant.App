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

            builder.HasOne(x => x.LinkedGoal)
                .WithMany(g => g.LinkedTasks)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Recurrence)
                .WithMany()
                .HasForeignKey(x => x.RecurrenceId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.Shares)
                .WithOne()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
