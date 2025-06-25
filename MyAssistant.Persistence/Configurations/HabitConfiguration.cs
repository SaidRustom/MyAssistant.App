using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MyAssistant.Domain.Models;

namespace MyAssistant.Persistence.Configurations
{
    public class HabitConfiguration : IEntityTypeConfiguration<Habit>
    {
        public void Configure(EntityTypeBuilder<Habit> builder)
        {
            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Description)
                .HasMaxLength(1000);

            builder.Property(x => x.GoalValue)
                .IsRequired();

            builder.HasOne(x => x.LinkedGoal)
                .WithMany(g => g.LinkedHabits)
                .HasForeignKey(x => x.GoalId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(x => x.Shares)
                .WithOne()
                .HasForeignKey(s => new { s.EntityId, s.EntityType })
                .HasPrincipalKey(h => new { EntityId = h.Id, EntityType = nameof(Habit) })
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.AuditLogs)
                .WithOne()
                .HasForeignKey(a => a.EntityId)
                .HasPrincipalKey(h => h.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
