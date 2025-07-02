using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MyAssistant.Domain.Models;

namespace MyAssistant.Persistence.Configurations
{
    public class GoalConfiguration : IEntityTypeConfiguration<Goal>
    {
        public void Configure(EntityTypeBuilder<Goal> builder)
        {
            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Description)
                .HasMaxLength(1000);

            builder.HasMany(x => x.LinkedTasks)
                .WithOne(t => t.LinkedGoal)
                .HasForeignKey(t => t.GoalId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.LinkedHabits)
                .WithOne(h => h.LinkedGoal)
                .HasForeignKey(h => h.GoalId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.Shares)
                .WithOne()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.Bills)
                .WithOne()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
