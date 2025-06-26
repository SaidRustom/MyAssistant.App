using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyAssistant.Domain.Models;

namespace MyAssistant.Persistence.Configurations
{
    public class BillingInfoConfiguration : IEntityTypeConfiguration<BillingInfo>
    {
        public void Configure(EntityTypeBuilder<BillingInfo> builder)
        {
            builder.HasMany(x => x.Shares)
                .WithOne()
                .HasForeignKey(s => s.EntityId)
                .HasPrincipalKey(g => g.Id)
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasMany(x => x.AuditLogs)
                .WithOne()
                .HasForeignKey(a => a.EntityId)
                .HasPrincipalKey(g => g.Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.ParentEntityId)
                .IsRequired();
        }
    }
}
