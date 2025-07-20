using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MyAssistant.Domain.Models;

namespace MyAssistant.Persistence.Configurations
{
    public class ShoppingListConfiguration : IEntityTypeConfiguration<ShoppingList>
    {
        public void Configure(EntityTypeBuilder<ShoppingList> builder)
        {
            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Description)
                .HasMaxLength(1000);

            builder.HasMany(x => x.Bills)
                .WithOne()
                .HasForeignKey(a => a.ParentEntityId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.Items)
                .WithOne(i => i.ShoppingList)
                .HasForeignKey(i => i.ShoppingListId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.AuditLogs)
                .WithOne()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
