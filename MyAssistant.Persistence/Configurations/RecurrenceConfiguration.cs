using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyAssistant.Domain.Models;

namespace MyAssistant.Persistence.Configurations
{
    public class RecurrenceConfiguration : IEntityTypeConfiguration<Recurrence>
    {
        public void Configure(EntityTypeBuilder<Recurrence> builder)
        {
            builder.HasOne(x => x.RecurrenceType)
                .WithMany()
                .HasForeignKey(x => x.RecurrenceTypeCode)
                .IsRequired();
        }
    }
}
