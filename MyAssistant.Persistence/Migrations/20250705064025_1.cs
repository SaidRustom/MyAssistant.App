using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyAssistant.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_entityShare_BillingInfo_BillingInfoId",
                table: "entityShare");

            migrationBuilder.DropForeignKey(
                name: "FK_entityShare_Goal_GoalId",
                table: "entityShare");

            migrationBuilder.DropForeignKey(
                name: "FK_entityShare_Habit_HabitId",
                table: "entityShare");

            migrationBuilder.DropForeignKey(
                name: "FK_entityShare_PermissionTypes_PermissionTypeCode",
                table: "entityShare");

            migrationBuilder.DropForeignKey(
                name: "FK_entityShare_ShoppingList_ShoppingListId",
                table: "entityShare");

            migrationBuilder.DropForeignKey(
                name: "FK_entityShare_TaskItem_TaskItemId",
                table: "entityShare");

            migrationBuilder.DropPrimaryKey(
                name: "PK_entityShare",
                table: "entityShare");

            migrationBuilder.RenameTable(
                name: "entityShare",
                newName: "EntityShare");

            migrationBuilder.RenameIndex(
                name: "IX_entityShare_TaskItemId",
                table: "EntityShare",
                newName: "IX_EntityShare_TaskItemId");

            migrationBuilder.RenameIndex(
                name: "IX_entityShare_ShoppingListId",
                table: "EntityShare",
                newName: "IX_EntityShare_ShoppingListId");

            migrationBuilder.RenameIndex(
                name: "IX_entityShare_PermissionTypeCode",
                table: "EntityShare",
                newName: "IX_EntityShare_PermissionTypeCode");

            migrationBuilder.RenameIndex(
                name: "IX_entityShare_HabitId",
                table: "EntityShare",
                newName: "IX_EntityShare_HabitId");

            migrationBuilder.RenameIndex(
                name: "IX_entityShare_GoalId",
                table: "EntityShare",
                newName: "IX_EntityShare_GoalId");

            migrationBuilder.RenameIndex(
                name: "IX_entityShare_EntityId_EntityType_SharedWithUserId",
                table: "EntityShare",
                newName: "IX_EntityShare_EntityId_EntityType_SharedWithUserId");

            migrationBuilder.RenameIndex(
                name: "IX_entityShare_BillingInfoId",
                table: "EntityShare",
                newName: "IX_EntityShare_BillingInfoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EntityShare",
                table: "EntityShare",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EntityShare_BillingInfo_BillingInfoId",
                table: "EntityShare",
                column: "BillingInfoId",
                principalTable: "BillingInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EntityShare_Goal_GoalId",
                table: "EntityShare",
                column: "GoalId",
                principalTable: "Goal",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EntityShare_Habit_HabitId",
                table: "EntityShare",
                column: "HabitId",
                principalTable: "Habit",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EntityShare_PermissionTypes_PermissionTypeCode",
                table: "EntityShare",
                column: "PermissionTypeCode",
                principalTable: "PermissionTypes",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EntityShare_ShoppingList_ShoppingListId",
                table: "EntityShare",
                column: "ShoppingListId",
                principalTable: "ShoppingList",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EntityShare_TaskItem_TaskItemId",
                table: "EntityShare",
                column: "TaskItemId",
                principalTable: "TaskItem",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntityShare_BillingInfo_BillingInfoId",
                table: "EntityShare");

            migrationBuilder.DropForeignKey(
                name: "FK_EntityShare_Goal_GoalId",
                table: "EntityShare");

            migrationBuilder.DropForeignKey(
                name: "FK_EntityShare_Habit_HabitId",
                table: "EntityShare");

            migrationBuilder.DropForeignKey(
                name: "FK_EntityShare_PermissionTypes_PermissionTypeCode",
                table: "EntityShare");

            migrationBuilder.DropForeignKey(
                name: "FK_EntityShare_ShoppingList_ShoppingListId",
                table: "EntityShare");

            migrationBuilder.DropForeignKey(
                name: "FK_EntityShare_TaskItem_TaskItemId",
                table: "EntityShare");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EntityShare",
                table: "EntityShare");

            migrationBuilder.RenameTable(
                name: "EntityShare",
                newName: "entityShare");

            migrationBuilder.RenameIndex(
                name: "IX_EntityShare_TaskItemId",
                table: "entityShare",
                newName: "IX_entityShare_TaskItemId");

            migrationBuilder.RenameIndex(
                name: "IX_EntityShare_ShoppingListId",
                table: "entityShare",
                newName: "IX_entityShare_ShoppingListId");

            migrationBuilder.RenameIndex(
                name: "IX_EntityShare_PermissionTypeCode",
                table: "entityShare",
                newName: "IX_entityShare_PermissionTypeCode");

            migrationBuilder.RenameIndex(
                name: "IX_EntityShare_HabitId",
                table: "entityShare",
                newName: "IX_entityShare_HabitId");

            migrationBuilder.RenameIndex(
                name: "IX_EntityShare_GoalId",
                table: "entityShare",
                newName: "IX_entityShare_GoalId");

            migrationBuilder.RenameIndex(
                name: "IX_EntityShare_EntityId_EntityType_SharedWithUserId",
                table: "entityShare",
                newName: "IX_entityShare_EntityId_EntityType_SharedWithUserId");

            migrationBuilder.RenameIndex(
                name: "IX_EntityShare_BillingInfoId",
                table: "entityShare",
                newName: "IX_entityShare_BillingInfoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_entityShare",
                table: "entityShare",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_entityShare_BillingInfo_BillingInfoId",
                table: "entityShare",
                column: "BillingInfoId",
                principalTable: "BillingInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_entityShare_Goal_GoalId",
                table: "entityShare",
                column: "GoalId",
                principalTable: "Goal",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_entityShare_Habit_HabitId",
                table: "entityShare",
                column: "HabitId",
                principalTable: "Habit",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_entityShare_PermissionTypes_PermissionTypeCode",
                table: "entityShare",
                column: "PermissionTypeCode",
                principalTable: "PermissionTypes",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_entityShare_ShoppingList_ShoppingListId",
                table: "entityShare",
                column: "ShoppingListId",
                principalTable: "ShoppingList",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_entityShare_TaskItem_TaskItemId",
                table: "entityShare",
                column: "TaskItemId",
                principalTable: "TaskItem",
                principalColumn: "Id");
        }
    }
}
