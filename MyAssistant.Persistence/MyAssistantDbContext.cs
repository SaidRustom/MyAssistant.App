using Microsoft.EntityFrameworkCore;
using MyAssistant.Domain.Models;
using MyAssistant.Domain.Base;
using MyAssistant.Domain.Lookups;
using MyAssistant.Core.Contracts;

namespace MyAssistant.Persistence
{
    public class MyAssistantDbContext : DbContext
    {
        private readonly ILoggedInUserService? _loggedInUserService;

        public MyAssistantDbContext(DbContextOptions<MyAssistantDbContext> options) : base(options) { }

        public MyAssistantDbContext(
            DbContextOptions<MyAssistantDbContext> options, ILoggedInUserService loggedInUserService) : base(options) 
        {
            _loggedInUserService = loggedInUserService;
        }

        //Entities
        public DbSet<TaskItem> TaskItems { get; set; } = default!;
        public DbSet<Recurrence> Recurrences { get; set; } = default!;
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

        //Lookups
        public DbSet<AuditActionType> AuditActionTypes { get; set; } = default!;
        public DbSet<PermissionType> PermissionTypes { get; set; } = default!;
        public DbSet<RecurrenceType> RecurrenceTypes { get; set; } = default!;
        public DbSet<ActivityType> ShoppingItemActivityTypes { get; set; } = default!;
        public DbSet<MyAssistantServiceType> MyAssistantServiceTypes { get; set; } = default!;

        /// <summary>
        /// Log created/updated date 
        /// </summary>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            if(_loggedInUserService == null)
                throw new NullReferenceException(nameof(_loggedInUserService));

            // Materialize the entries first to avoid issues as the ChangeTracker could change during foreach
            var entries = ChangeTracker.Entries<EntityBase>().ToList();

            foreach (var entry in entries)
            {
                // Get the correct entity type (handles proxies)
                var entityType = entry.Metadata.ClrType;
                // Find the corresponding EF entity type (may return null for unmapped types)
                var efEntityType = entry.Context.Model.FindEntityType(entityType);
                // Get the table name if found, or fallback to type name
                var tableName = efEntityType?.GetTableName() ?? entityType.Name;

                if (entry.State == EntityState.Added) //add the user id
                {
                    entry.Entity.Id = Guid.NewGuid();
                    entry.Entity.UserId = _loggedInUserService.UserId;

                    var auditLog = new AuditLog(tableName, entry.Entity, _loggedInUserService.UserId, AuditActionType.Create);

                    await AuditLogs.AddAsync(auditLog, cancellationToken);
                }

                if (entry.State == EntityState.Modified)
                {
                    var auditLog = new AuditLog(tableName, entry.Entity, _loggedInUserService.UserId, AuditActionType.Update);

                    // Load current database values for comparison
                    var dbValues = await entry.GetDatabaseValuesAsync(cancellationToken);
                    // And make sure UserId doesn't change..
                    entry.Entity.UserId = Guid.Parse(dbValues["UserId"].ToString() ?? Guid.Empty.ToString());
                    var changes = new List<HistoryEntry>();

                    //Check if the object was changed..
                    foreach (var prop in entry.Properties)
                    {
                        var dbValue = dbValues?[prop.Metadata.Name];
                        var currentValue = prop.CurrentValue;

                        // Compare DB value and current value
                        if (!object.Equals(dbValue, currentValue))
                        {
                            var change = new HistoryEntry(auditLog)
                            {
                                PropertyName = prop.Metadata.Name,
                                OldValue = dbValue?.ToString(),
                                NewValue = currentValue?.ToString(),
                            };
                            changes.Add(change);
                        }
                    }
             
                    if (changes.Count > 0) //Add Audit & Change entities to db
                    {
                        await AuditLogs.AddAsync(auditLog, cancellationToken);
                        await HistoryEntries.AddRangeAsync(changes);
                        //TODO: Add notification handling here
                    }
                }
                //TODO: Handle delete
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// The method to be called by background services to handle audit logs accordingly
        /// </summary>
        /// <remarks>UserId should be populated before calling this</remarks>
        public async Task<int> SaveServiceChangesAsync(MyAssistantServiceType serviceType, CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<EntityBase>().ToList();

            foreach (var entry in entries)
            {
                // Get the correct entity type (handles proxies)
                var entityType = entry.Metadata.ClrType;
                // Find the corresponding EF entity type (may return null for unmapped types)
                var efEntityType = entry.Context.Model.FindEntityType(entityType);
                // Get the table name if found, or fallback to type name
                var tableName = efEntityType?.GetTableName() ?? entityType.Name;

                if (entry.State == EntityState.Added) //add the user id
                {
                    entry.Entity.Id = Guid.NewGuid();
                    var auditLog = new AuditLog(tableName, entry.Entity, serviceType, AuditActionType.Create);

                    await AuditLogs.AddAsync(auditLog, cancellationToken);
                }

                if (entry.State == EntityState.Modified)
                {
                    var auditLog = new AuditLog(tableName, entry.Entity, serviceType, AuditActionType.Update);

                    // Load current database values for comparison
                    var dbValues = await entry.GetDatabaseValuesAsync(cancellationToken);
                    var changes = new List<HistoryEntry>();

                    //Check if the object was changed..
                    foreach (var prop in entry.Properties)
                    {
                        var dbValue = dbValues?[prop.Metadata.Name];
                        var currentValue = prop.CurrentValue;

                        // Compare DB value and current value
                        if (!object.Equals(dbValue, currentValue))
                        {
                            var change = new HistoryEntry(auditLog)
                            {
                                PropertyName = prop.Metadata.Name,
                                OldValue = dbValue?.ToString(),
                                NewValue = currentValue?.ToString(),
                            };
                            changes.Add(change);
                        }
                    }

                    if (changes.Count > 0) //Add Audit & Change entities to db
                    {
                        await AuditLogs.AddAsync(auditLog, cancellationToken);
                        await HistoryEntries.AddRangeAsync(changes);
                    }
                }
                //TODO: Handle delete
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// USE WITH CAUTION!! used to Save ServiceLogs etc
        /// </summary>
        public async Task<int> SurpassAuditAndSaveAsync(CancellationToken cancellationToken = default)
        {
            //TODO: Create a seperate DbContext for logging and remove this..
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyAssistantDbContext).Assembly);

            #region Seed Lookups

            //Recurrence Type
            modelBuilder.Entity<RecurrenceType>().HasData(new RecurrenceType { Code = 1, Description = "None" });
            modelBuilder.Entity<RecurrenceType>().HasData(new RecurrenceType  { Code = 2, Description = "Hourly" });
            modelBuilder.Entity<RecurrenceType>().HasData(new RecurrenceType { Code = 3, Description = "Daily" });
            modelBuilder.Entity<RecurrenceType>().HasData(new RecurrenceType { Code = 4, Description = "Weekly" });
            modelBuilder.Entity<RecurrenceType>().HasData(new RecurrenceType { Code = 5, Description = "Monthly" });
            modelBuilder.Entity<RecurrenceType>().HasData(new RecurrenceType { Code = 6, Description = "Annually" });

            //Audit Action Type
            modelBuilder.Entity<AuditActionType>().HasData(new AuditActionType { Code = 1, Description = "Create" });
            modelBuilder.Entity<AuditActionType>().HasData(new AuditActionType { Code = 2, Description = "Update" });
            modelBuilder.Entity<AuditActionType>().HasData(new AuditActionType { Code = 3, Description = "Delete" });

            //Permission Type
            modelBuilder.Entity<PermissionType>().HasData(new PermissionType { Code = 1, Description = "Read" });
            modelBuilder.Entity<PermissionType>().HasData(new PermissionType { Code = 2, Description = "Read/Write" });
            modelBuilder.Entity<PermissionType>().HasData(new PermissionType { Code = 3, Description = "Read/Write/Delete" });

            //Activity Type
            modelBuilder.Entity<ActivityType>().HasData(new ActivityType { Code = 1, Description = "Active"});
            modelBuilder.Entity<ActivityType>().HasData(new ActivityType { Code = 2, Description = "Inactive" });
            modelBuilder.Entity<ActivityType>().HasData(new ActivityType { Code = 3, Description = "Urgent" });
            modelBuilder.Entity<ActivityType>().HasData(new ActivityType { Code = 4, Description = "NotUrgent" });

            //Shopping List Type
            modelBuilder.Entity<ShoppingListType>().HasData(
                new ShoppingListType { Code = 1, Description = "Groceries" },
                new ShoppingListType { Code = 2, Description = "Pharmaceuticals" },
                new ShoppingListType { Code = 3, Description = "Electronics" },
                new ShoppingListType { Code = 4, Description = "Clothing" },
                new ShoppingListType { Code = 5, Description = "HomeGoods" },
                new ShoppingListType { Code = 6, Description = "Beauty" },
                new ShoppingListType { Code = 7, Description = "Toys" },
                new ShoppingListType { Code = 8, Description = "Books" },
                new ShoppingListType { Code = 9, Description = "Office Supplies" },
                new ShoppingListType { Code = 10, Description = "Sports Equipment" },
                new ShoppingListType { Code = 11, Description = "Automotive" },
                new ShoppingListType { Code = 12, Description = "Pet Supplies" },
                new ShoppingListType { Code = 13, Description = "Garden" },
                new ShoppingListType { Code = 14, Description = "Baby Products" },
                new ShoppingListType { Code = 15, Description = "Furniture" }
                );

            modelBuilder.Entity<MyAssistantServiceType>().HasData(new MyAssistantServiceType { Code = 1, Description = "Recurring Shopping List Item Activation Service" });

            #endregion
        }
    }
}
