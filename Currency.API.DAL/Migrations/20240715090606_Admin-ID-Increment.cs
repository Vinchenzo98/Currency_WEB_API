using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Currency.API.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AdminIDIncrement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlockedUsers_AccountType_AccountID",
                table: "BlockedUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_BlockedUsers_Admins_AdminID",
                table: "BlockedUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_BlockedUsers_Users_UserID",
                table: "BlockedUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlockedUsers",
                table: "BlockedUsers");

            migrationBuilder.RenameTable(
                name: "BlockedUsers",
                newName: "BlockedUserLog");

            migrationBuilder.RenameIndex(
                name: "IX_BlockedUsers_UserID",
                table: "BlockedUserLog",
                newName: "IX_BlockedUserLog_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_BlockedUsers_AdminID",
                table: "BlockedUserLog",
                newName: "IX_BlockedUserLog_AdminID");

            migrationBuilder.AddColumn<DateTime>(
                name: "UnBlockedTime",
                table: "BlockedTransactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Reason",
                table: "BlockedUserLog",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlockedUserLog",
                table: "BlockedUserLog",
                columns: new[] { "AccountID", "UserID", "AdminID", "BlockDate" });

            migrationBuilder.AddForeignKey(
                name: "FK_BlockedUserLog_AccountType_AccountID",
                table: "BlockedUserLog",
                column: "AccountID",
                principalTable: "AccountType",
                principalColumn: "AccountID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlockedUserLog_Admins_AdminID",
                table: "BlockedUserLog",
                column: "AdminID",
                principalTable: "Admins",
                principalColumn: "AdminID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlockedUserLog_Users_UserID",
                table: "BlockedUserLog",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlockedUserLog_AccountType_AccountID",
                table: "BlockedUserLog");

            migrationBuilder.DropForeignKey(
                name: "FK_BlockedUserLog_Admins_AdminID",
                table: "BlockedUserLog");

            migrationBuilder.DropForeignKey(
                name: "FK_BlockedUserLog_Users_UserID",
                table: "BlockedUserLog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlockedUserLog",
                table: "BlockedUserLog");

            migrationBuilder.DropColumn(
                name: "UnBlockedTime",
                table: "BlockedTransactions");

            migrationBuilder.DropColumn(
                name: "Reason",
                table: "BlockedUserLog");

            migrationBuilder.RenameTable(
                name: "BlockedUserLog",
                newName: "BlockedUsers");

            migrationBuilder.RenameIndex(
                name: "IX_BlockedUserLog_UserID",
                table: "BlockedUsers",
                newName: "IX_BlockedUsers_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_BlockedUserLog_AdminID",
                table: "BlockedUsers",
                newName: "IX_BlockedUsers_AdminID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlockedUsers",
                table: "BlockedUsers",
                columns: new[] { "AccountID", "UserID", "AdminID", "BlockDate" });

            migrationBuilder.AddForeignKey(
                name: "FK_BlockedUsers_AccountType_AccountID",
                table: "BlockedUsers",
                column: "AccountID",
                principalTable: "AccountType",
                principalColumn: "AccountID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlockedUsers_Admins_AdminID",
                table: "BlockedUsers",
                column: "AdminID",
                principalTable: "Admins",
                principalColumn: "AdminID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlockedUsers_Users_UserID",
                table: "BlockedUsers",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
