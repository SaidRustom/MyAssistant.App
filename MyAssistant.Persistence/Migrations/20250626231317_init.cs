using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyAssistant.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditActionTypes",
                columns: table => new
                {
                    Code = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditActionTypes", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "ChatMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceiverUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    ReadAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AttachmentUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    MessageType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Goals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    TargetDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsAchieved = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    ReadAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActionUrl = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    EmailNotification = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PermissionTypes",
                columns: table => new
                {
                    Code = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionTypes", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "RecurrenceTypes",
                columns: table => new
                {
                    Code = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecurrenceTypes", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingItemActivityTypes",
                columns: table => new
                {
                    Code = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingItemActivityTypes", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingLists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserConnections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FriendUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RequestStatus = table.Column<int>(type: "int", nullable: false),
                    RequestSentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StatusMessage = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserConnections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Habits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    GoalValue = table.Column<int>(type: "int", nullable: false),
                    Progress = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    IsRecurring = table.Column<bool>(type: "bit", nullable: false),
                    RecurrenceTypeCode = table.Column<int>(type: "int", nullable: true),
                    RecurrenceEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GoalId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Habits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Habits_Goals_GoalId",
                        column: x => x.GoalId,
                        principalTable: "Goals",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TaskItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    IsRecurring = table.Column<bool>(type: "bit", nullable: false),
                    RecurrenceTypeCode = table.Column<int>(type: "int", maxLength: 100, nullable: true),
                    RecurrenceEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GoalId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskItems_Goals_GoalId",
                        column: x => x.GoalId,
                        principalTable: "Goals",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ShoppingListItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LastPurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsRecurring = table.Column<bool>(type: "bit", nullable: false),
                    RecurrenceTypeCode = table.Column<int>(type: "int", nullable: true),
                    RecurrenceEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ShoppingListId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingListItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingListItems_ShoppingLists_ShoppingListId",
                        column: x => x.ShoppingListId,
                        principalTable: "ShoppingLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BillingInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentEntityType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    PaidAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GoalId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    HabitId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillingInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillingInfo_Goals_GoalId",
                        column: x => x.GoalId,
                        principalTable: "Goals",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BillingInfo_Habits_HabitId",
                        column: x => x.HabitId,
                        principalTable: "Habits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BillingInfo_ShoppingLists_ParentEntityId",
                        column: x => x.ParentEntityId,
                        principalTable: "ShoppingLists",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntityType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActionTypeCode = table.Column<int>(type: "int", nullable: false),
                    BillingInfoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ChatMessageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GoalId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    HabitId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NotificationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ShoppingListId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TaskItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserConnectionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditLogs_BillingInfo_BillingInfoId",
                        column: x => x.BillingInfoId,
                        principalTable: "BillingInfo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AuditLogs_ChatMessages_ChatMessageId",
                        column: x => x.ChatMessageId,
                        principalTable: "ChatMessages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AuditLogs_Goals_GoalId",
                        column: x => x.GoalId,
                        principalTable: "Goals",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AuditLogs_Habits_HabitId",
                        column: x => x.HabitId,
                        principalTable: "Habits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AuditLogs_Notifications_NotificationId",
                        column: x => x.NotificationId,
                        principalTable: "Notifications",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AuditLogs_ShoppingLists_ShoppingListId",
                        column: x => x.ShoppingListId,
                        principalTable: "ShoppingLists",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AuditLogs_TaskItems_TaskItemId",
                        column: x => x.TaskItemId,
                        principalTable: "TaskItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AuditLogs_UserConnections_UserConnectionId",
                        column: x => x.UserConnectionId,
                        principalTable: "UserConnections",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "entityShare",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntityType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SharedWithUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissionTypeCode = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    SharedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BillingInfoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GoalId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    HabitId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ShoppingListId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TaskItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entityShare", x => x.Id);
                    table.ForeignKey(
                        name: "FK_entityShare_BillingInfo_BillingInfoId",
                        column: x => x.BillingInfoId,
                        principalTable: "BillingInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_entityShare_Goals_GoalId",
                        column: x => x.GoalId,
                        principalTable: "Goals",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_entityShare_Habits_HabitId",
                        column: x => x.HabitId,
                        principalTable: "Habits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_entityShare_PermissionTypes_PermissionTypeCode",
                        column: x => x.PermissionTypeCode,
                        principalTable: "PermissionTypes",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_entityShare_ShoppingLists_ShoppingListId",
                        column: x => x.ShoppingListId,
                        principalTable: "ShoppingLists",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_entityShare_TaskItems_TaskItemId",
                        column: x => x.TaskItemId,
                        principalTable: "TaskItems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "historyEntry",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuditLogId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    OldValue = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    NewValue = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_historyEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_historyEntry_AuditLogs_AuditLogId",
                        column: x => x.AuditLogId,
                        principalTable: "AuditLogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AuditActionTypes",
                columns: new[] { "Code", "Description" },
                values: new object[,]
                {
                    { 1, "Create" },
                    { 2, "Update" },
                    { 3, "Delete" }
                });

            migrationBuilder.InsertData(
                table: "PermissionTypes",
                columns: new[] { "Code", "Description" },
                values: new object[,]
                {
                    { 1, "Read" },
                    { 2, "Read/Write" },
                    { 3, "Read/Write/Delete" }
                });

            migrationBuilder.InsertData(
                table: "RecurrenceTypes",
                columns: new[] { "Code", "Description" },
                values: new object[,]
                {
                    { 1, "None" },
                    { 2, "Hourly" },
                    { 3, "Daily" },
                    { 4, "Weekly" },
                    { 5, "Monthly" },
                    { 6, "Annually" }
                });

            migrationBuilder.InsertData(
                table: "ShoppingItemActivityTypes",
                columns: new[] { "Code", "Description" },
                values: new object[,]
                {
                    { 1, "Active" },
                    { 2, "Inactive" },
                    { 3, "Urgent" },
                    { 4, "NotUrgent" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_BillingInfoId",
                table: "AuditLogs",
                column: "BillingInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_ChatMessageId",
                table: "AuditLogs",
                column: "ChatMessageId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_GoalId",
                table: "AuditLogs",
                column: "GoalId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_HabitId",
                table: "AuditLogs",
                column: "HabitId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_NotificationId",
                table: "AuditLogs",
                column: "NotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_ShoppingListId",
                table: "AuditLogs",
                column: "ShoppingListId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_TaskItemId",
                table: "AuditLogs",
                column: "TaskItemId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_UserConnectionId",
                table: "AuditLogs",
                column: "UserConnectionId");

            migrationBuilder.CreateIndex(
                name: "IX_BillingInfo_GoalId",
                table: "BillingInfo",
                column: "GoalId");

            migrationBuilder.CreateIndex(
                name: "IX_BillingInfo_HabitId",
                table: "BillingInfo",
                column: "HabitId");

            migrationBuilder.CreateIndex(
                name: "IX_BillingInfo_ParentEntityId",
                table: "BillingInfo",
                column: "ParentEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_UserId_ReceiverUserId_SentAt",
                table: "ChatMessages",
                columns: new[] { "UserId", "ReceiverUserId", "SentAt" });

            migrationBuilder.CreateIndex(
                name: "IX_entityShare_BillingInfoId",
                table: "entityShare",
                column: "BillingInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_entityShare_EntityId_EntityType_SharedWithUserId",
                table: "entityShare",
                columns: new[] { "EntityId", "EntityType", "SharedWithUserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_entityShare_GoalId",
                table: "entityShare",
                column: "GoalId");

            migrationBuilder.CreateIndex(
                name: "IX_entityShare_HabitId",
                table: "entityShare",
                column: "HabitId");

            migrationBuilder.CreateIndex(
                name: "IX_entityShare_PermissionTypeCode",
                table: "entityShare",
                column: "PermissionTypeCode");

            migrationBuilder.CreateIndex(
                name: "IX_entityShare_ShoppingListId",
                table: "entityShare",
                column: "ShoppingListId");

            migrationBuilder.CreateIndex(
                name: "IX_entityShare_TaskItemId",
                table: "entityShare",
                column: "TaskItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Habits_GoalId",
                table: "Habits",
                column: "GoalId");

            migrationBuilder.CreateIndex(
                name: "IX_historyEntry_AuditLogId",
                table: "historyEntry",
                column: "AuditLogId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingListItems_ShoppingListId",
                table: "ShoppingListItems",
                column: "ShoppingListId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskItems_GoalId",
                table: "TaskItems",
                column: "GoalId");

            migrationBuilder.CreateIndex(
                name: "IX_UserConnections_UserId_FriendUserId",
                table: "UserConnections",
                columns: new[] { "UserId", "FriendUserId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditActionTypes");

            migrationBuilder.DropTable(
                name: "entityShare");

            migrationBuilder.DropTable(
                name: "historyEntry");

            migrationBuilder.DropTable(
                name: "RecurrenceTypes");

            migrationBuilder.DropTable(
                name: "ShoppingItemActivityTypes");

            migrationBuilder.DropTable(
                name: "ShoppingListItems");

            migrationBuilder.DropTable(
                name: "PermissionTypes");

            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "BillingInfo");

            migrationBuilder.DropTable(
                name: "ChatMessages");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "TaskItems");

            migrationBuilder.DropTable(
                name: "UserConnections");

            migrationBuilder.DropTable(
                name: "Habits");

            migrationBuilder.DropTable(
                name: "ShoppingLists");

            migrationBuilder.DropTable(
                name: "Goals");
        }
    }
}
