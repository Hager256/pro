using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotivAi.Migrations
{
    /// <inheritdoc />
    public partial class AddChatHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Habits_Habit_Frequencies_Frequency_id1",
                table: "Habits");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Notification_Types_Type_id1",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_Type_id1",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Habits_Frequency_id1",
                table: "Habits");

            migrationBuilder.DropColumn(
                name: "Type_id1",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "Frequency_id1",
                table: "Habits");

            migrationBuilder.CreateTable(
                name: "ChatHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BotReply = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatHistories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_Type_id",
                table: "Notifications",
                column: "Type_id");

            migrationBuilder.CreateIndex(
                name: "IX_Habits_Frequency_id",
                table: "Habits",
                column: "Frequency_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Habits_Habit_Frequencies_Frequency_id",
                table: "Habits",
                column: "Frequency_id",
                principalTable: "Habit_Frequencies",
                principalColumn: "Frequency_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Notification_Types_Type_id",
                table: "Notifications",
                column: "Type_id",
                principalTable: "Notification_Types",
                principalColumn: "Type_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Habits_Habit_Frequencies_Frequency_id",
                table: "Habits");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Notification_Types_Type_id",
                table: "Notifications");

            migrationBuilder.DropTable(
                name: "ChatHistories");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_Type_id",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Habits_Frequency_id",
                table: "Habits");

            migrationBuilder.AddColumn<int>(
                name: "Type_id1",
                table: "Notifications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Frequency_id1",
                table: "Habits",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_Type_id1",
                table: "Notifications",
                column: "Type_id1");

            migrationBuilder.CreateIndex(
                name: "IX_Habits_Frequency_id1",
                table: "Habits",
                column: "Frequency_id1");

            migrationBuilder.AddForeignKey(
                name: "FK_Habits_Habit_Frequencies_Frequency_id1",
                table: "Habits",
                column: "Frequency_id1",
                principalTable: "Habit_Frequencies",
                principalColumn: "Frequency_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Notification_Types_Type_id1",
                table: "Notifications",
                column: "Type_id1",
                principalTable: "Notification_Types",
                principalColumn: "Type_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
