 using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Api.Migrations
{
    /// <inheritdoc />
    public partial class Changednameoftasksfields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_Account_UserId",
                table: "Task");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Task",
                newName: "AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Task_UserId",
                table: "Task",
                newName: "IX_Task_AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Task_Account_AccountId",
                table: "Task",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_Account_AccountId",
                table: "Task");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "Task",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Task_AccountId",
                table: "Task",
                newName: "IX_Task_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Task_Account_UserId",
                table: "Task",
                column: "UserId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
