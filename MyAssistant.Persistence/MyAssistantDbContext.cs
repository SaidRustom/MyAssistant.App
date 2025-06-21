using Microsoft.EntityFrameworkCore;
using MyAssistant.Domain.Models;
using MyAssistant.Domain.Base;

namespace MyAssistant.Persistence
{
    public class MyAssistantDbContext : DbContext
    {
        public MyAssistantDbContext(
            DbContextOptions<MyAssistantDbContext> options) : base(options) { }

        public DbSet<TaskItem> TaskItems { get; set; } = default!;
        public DbSet<Habit> Habits { get; set; } = default!;
        public DbSet<ShoppingList> ShoppingLists { get; set; } = default!;
        public DbSet<ShoppingListItem> ShoppingListItems { get; set; } = default!;
        public DbSet<Goal> Goals { get; set; } = default!;
        public DbSet<UserConnection> UserConnections { get; set; } = default!;
        public DbSet<Notification> Notifications { get; set; } = default!;
        public DbSet<ChatMessage> ChatMessages { get; set; } = default!;
        public DbSet<EntityShare> EntityShares { get; set; } = default!;
        public DbSet<AuditLog> AuditLogs { get; set; } = default!;
        public DbSet<HistoryEntry> HistoryEntries { get; set; } = default!;


        /// <summary>
        /// Log created/updated date 
        /// </summary>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var changedEntries = ChangeTracker.Entries<EntityBase>();

            foreach (var entry in changedEntries)
            {
                var auditLog = new AuditLog(entry.Entity);
                // auditLog.UserId = IMPLEMENT THIS!

                switch (entry.State)
                {
                    case EntityState.Added:
                        auditLog.Action = AuditLog.Actions.Create;
                        break;
                    case EntityState.Modified:
                        auditLog.Action = AuditLog.Actions.Update;
                        break;
                    case EntityState.Deleted:
                        auditLog.Action = AuditLog.Actions.Delete;
                        break;
                }

                foreach (var prop in entry.Properties)
                {
                    if (prop.IsModified)
                    {
                        var change = new HistoryEntry(auditLog)
                        {
                            PropertyName = prop.Metadata.Name,
                            OldValue = prop.OriginalValue?.ToString(),
                            NewValue = prop.CurrentValue?.ToString(),
                        };
                    }
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
