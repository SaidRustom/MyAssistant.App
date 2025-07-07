using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyAssistant.Domain.Base;

namespace MyAssistant.Persistence.Configurations
{
    public class MyAssistantServiceLogConfiguration: IEntityTypeConfiguration<MyAssistantServiceLog>
    {
        public void Configure(EntityTypeBuilder<MyAssistantServiceLog> builder)
        {
            builder.HasOne(x => x.MyAssistantServiceType)
                .WithMany()
                .HasForeignKey(a => a.MyAssistantServiceTypeCode)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
